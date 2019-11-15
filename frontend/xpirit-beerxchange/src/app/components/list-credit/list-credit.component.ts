import { Component, OnInit } from '@angular/core';
import { Subscription, timer } from 'rxjs';
import { FridgeService } from 'src/app/services/fridge.service';
import { UserCredits } from '../../model/userCredits';

@Component({
  selector: 'list-credit',
  templateUrl: './list-credit.component.html',
  styleUrls: ['./list-credit.component.css']
})
export class ListCreditComponent implements OnInit {
  displayedColumns: string[] = ['name', 'credits', 'beersAdded', 'beersTaken'];
  private readonly _subscription: Subscription = new Subscription();

  constructor(private fridgeService: FridgeService) { }
  
  userCredits: Array<UserCredits> = [];

  ngOnInit() {
    this._subscription.add(timer(0, 30000).subscribe(() => {
      this.setUserCreditList();
    }));
  }

  setUserCreditList() {
    this._subscription.add(this.fridgeService.getUserCredits().subscribe((userCredits: Array<UserCredits>) => {
      this.userCredits = userCredits.sort((x,y) => x.name > y.name ? 1 : -1);
    }));
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }

}

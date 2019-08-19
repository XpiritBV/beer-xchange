import { Component, OnInit } from '@angular/core';
import { FridgeService } from '../../services/fridge.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-view-credit',
  templateUrl: './view-credit.component.html',
  styleUrls: ['./view-credit.component.css']
})
export class ViewCreditComponent implements OnInit {
  private readonly _subscription: Subscription = new Subscription();
  credits: number;

  constructor(private fridgeService: FridgeService) { }

  ngOnInit() {

    this._subscription.add(
        this.fridgeService.getCreditsForCurrentUser("geert van der Cruijsen").subscribe((credits: number) => {
            this.credits = credits;
        })
    );
  }

}

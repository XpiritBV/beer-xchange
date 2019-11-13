import { Component, OnInit, OnDestroy } from '@angular/core';
import { FridgeService } from 'src/app/services/fridge.service';
import { Beer } from 'src/app/model/beer';
import { Subscription, timer } from 'rxjs';

@Component({
  selector: 'app-fridge-beerlist-current',
  templateUrl: './fridge-beerlist-current.component.html',
  styleUrls: ['./fridge-beerlist-current.component.css']
})
export class FridgeBeerlistCurrentComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = ['name', 'brewery', 'country', 'createdBy', 'addedDate'];
  private readonly _subscription: Subscription = new Subscription();

  constructor(private fridgeService: FridgeService) { }
  
  beers: Array<Beer> = [];

  ngOnInit() {
    this._subscription.add(timer(0, 30000).subscribe(() => {
      this.setBeerList();
    }));
  }

  setBeerList() {
    this._subscription.add(this.fridgeService.getCurrentBeers().subscribe((beers: Array<Beer>) => {
      this.beers = beers;
    }));
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }
}

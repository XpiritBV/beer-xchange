import { Component, OnInit } from '@angular/core';
import { Subscription, timer } from 'rxjs';
import { FridgeService } from 'src/app/services/fridge.service';
import { Beer } from 'src/app/model/beer';

@Component({
  selector: 'app-fridge-beerlist-history',
  templateUrl: './fridge-beerlist-history.component.html',
  styleUrls: ['./fridge-beerlist-history.component.css']
})
export class FridgeBeerlistHistoryComponent implements OnInit {

  private readonly _subscription: Subscription = new Subscription();

  constructor(private fridgeService: FridgeService) { }
  
  beers: Array<Beer> = [];

  ngOnInit() {
    this._subscription.add(timer(0, 30000).subscribe(() => {
      this.setBeerList();
    }));
  }

  setBeerList() {
    this._subscription.add(this.fridgeService.getHistoricalBeers().subscribe((beers: Array<Beer>) => {
      this.beers = beers;
    }));
  }

}

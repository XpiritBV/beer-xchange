import { Component, OnInit, OnDestroy } from '@angular/core';
import { FridgeService } from 'src/app/services/fridge.service';
import { Beer } from 'src/app/model/beer';
import { Subscription, timer } from 'rxjs';
import { ExplainationResult } from 'src/app/model/ExplainationResult';

@Component({
  selector: 'app-fridge-beerlist-current',
  templateUrl: './fridge-beerlist-current.component.html',
  styleUrls: ['./fridge-beerlist-current.component.css']
})
export class FridgeBeerlistCurrentComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = ['name', 'brewery', 'country', 'createdBy', 'addedDate', 'explaination'];
  private readonly _subscription: Subscription = new Subscription();

  constructor(private fridgeService: FridgeService) { }

  beers: Array<Beer> = [];

  beerExplanation: String = "";

  ngOnInit() {
    this._subscription.add(timer(0, 30000).subscribe(() => {
      this.setBeerList();
    }));
  }

  setBeerList() {
    this._subscription.add(this.fridgeService.getCurrentBeers().subscribe((beers: Array<Beer>) => {
      // sort by added date
      this.beers = beers.sort((a, b) => {
        return new Date(b.addedDate).getTime() - new Date(a.addedDate).getTime();
      });
    }));
  }

  explainBeer(beer: Beer) {
    this._subscription.add(this.fridgeService.explainBeer(beer).subscribe((result: ExplainationResult) => {
      this.beerExplanation = result.explaination;
    }));
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }
}

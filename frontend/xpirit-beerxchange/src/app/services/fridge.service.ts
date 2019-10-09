import { Injectable } from '@angular/core';
import { AppConfig } from '../app.config';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Beer } from '../model/beer';
import { BeerAddition } from '../model/beerAddition';


@Injectable({
  providedIn: 'root'
})
export class FridgeService {

  constructor(private http: HttpClient) {
  }

  getCreditsForCurrentUser(user: string): Observable<number> {
    return this.http.get<number>(`${AppConfig.settings.apiUrl}/credit/${user}`);
  }

  getCurrentBeers(): Observable<Array<Beer>>{
    return this.http.get<Array<Beer>>(`${AppConfig.settings.apiUrl}/beer`).map(beers => beers.filter(b => b.removedBy == null));
  }

  getHistoricalBeers(): Observable<Array<Beer>>{
    return this.http.get<Array<Beer>>(`${AppConfig.settings.apiUrl}/beer`).map(beers => beers.filter(b => b.removedBy != null));
  }

  // ToBe updated
  takeBeer(beerId: number): Observable<void>{
    return this.http.put<void>(`${AppConfig.settings.apiUrl}/beer`, beerId);
  }

  getFridgeUsers(): Observable<Array<string>>{
    return this.http.get<Array<string>>(`${AppConfig.settings.apiUrl}/user`);
  }

  addBeer(beer: BeerAddition){
    console.log(beer);
    this.http.post(`${AppConfig.settings.apiUrl}/beeraddition`, beer)
        .subscribe(res => console.log('Done'));
  }

}

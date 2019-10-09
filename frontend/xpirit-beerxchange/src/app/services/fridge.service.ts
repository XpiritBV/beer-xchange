import { Injectable } from '@angular/core';
import { AppConfig } from '../app.config';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Beer } from '../model/beer';
import { BeerAddition } from '../model/beerAddition';
import { BeerRemoval } from '../model/beerRemoval';
import { CreditTransfer } from '../model/creditTransfer';


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

  addBeer(beer: BeerAddition){
    console.log(beer);
    this.http.post(`${AppConfig.settings.apiUrl}/beeraddition`, beer)
        .subscribe(res => console.log('Done'));
  }


  takeBeer(beerRemoval: BeerRemoval): Observable<void>{
    return this.http.post<void>(`${AppConfig.settings.apiUrl}/beerremoval`, beerRemoval);
  }

  transferCredit(creditTransfer: CreditTransfer): Observable<void>{
    return this.http.post<void>(`${AppConfig.settings.apiUrl}/credittransfer`, creditTransfer);
  }

  getFridgeUsers(): Observable<Array<string>>{
    return this.http.get<Array<string>>(`${AppConfig.settings.apiUrl}/user`);
  }

}

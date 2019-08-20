import { Injectable } from '@angular/core';
import { AppConfig } from '../app.config';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Beer } from '../model/beer';


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


  getFridgeUsers(): Observable<Array<string>>{
    return this.http.get<Array<string>>(`${AppConfig.settings.apiUrl}/user`);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BeerService {

  constructor(
    private http: HttpClient,
  ) { }

  getBeers() {
    var beers =  this.http.get<Array<String>>('https://localhost:44388/api/values').subscribe((beers) => alert(beers));
    return beers;
  }
}

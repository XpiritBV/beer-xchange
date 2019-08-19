import { Injectable } from '@angular/core';
import { AppConfig } from '../app.config';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class FridgeService {

  constructor(private http: HttpClient) {
  }

  getCreditsForCurrentUser(user: string): Observable<number> {
    return this.http.get<number>(`${AppConfig.settings.apiUrl}/credit/${user}`);
  }
}

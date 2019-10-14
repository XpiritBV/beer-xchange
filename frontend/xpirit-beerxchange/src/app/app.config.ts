import { Injectable } from '@angular/core'; 
import { Observable, BehaviorSubject } from 'rxjs'; 
import { tap } from 'rxjs/operators'; 
import { HttpBackend, HttpClient } from '@angular/common/http'; 
import { environment } from 'src/environments/environment';
import { IAppConfig } from './model/app-config';


@Injectable({ providedIn: 'root'})
export class AppConfig { 
    private appConfigSubject = new BehaviorSubject<any>(null); 
    static settings: IAppConfig;
    private httpClient: HttpClient;
    
    constructor(httpBackend: HttpBackend) { 
        this.httpClient = new HttpClient(httpBackend); 
    } 
        
    loadAppConfig(): Observable<any> { 
        const configUrl = `assets/config/config.${environment.name}.json?v=3`;

        return this.httpClient.get(configUrl).pipe(tap(response => {
            AppConfig.settings = response;
            this.appConfigSubject.next(response);
        })); 
    }
}
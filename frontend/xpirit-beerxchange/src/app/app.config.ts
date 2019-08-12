import { Injectable, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../environments/environment';
import { IAppConfig } from './model/app-config';
import { Subscription, Subject } from 'rxjs';

@Injectable()
export class AppConfig implements OnDestroy {
    subscribtion: Subscription = new Subscription();
    static settings: Subject<IAppConfig> = new Subject();
    private settingsSet: boolean = false;

    constructor(private http: HttpClient) {}

    load() {
        if (!this.settingsSet) {
            const jsonFile = `assets/config/config.${environment.name}.json`;

            this.subscribtion.add(this.http.get(jsonFile).subscribe((response: IAppConfig) => {
                debugger;
                AppConfig.settings.next(<IAppConfig>response);
                this.settingsSet = true;
            }));
        }
    }
    
    ngOnDestroy(): void {
        this.subscribtion.unsubscribe();
    }
}
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MsalModule, MsalInterceptor, MsalConfig } from '@azure/msal-angular';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BeerService } from './beer.service';
import { AppConfig } from './app.config';
import { MSAL_CONFIG, MsalService } from "@azure/msal-angular/dist/msal.service";
import { IAppConfig } from './model/app-config';
import { tap } from 'rxjs/operators';
import { HomeComponent } from './components/home/home.component';
import { AddBeerComponent } from './components/add-beer/add-beer.component';
import { ViewCreditComponent } from './components/view-credit/view-credit.component';
import { FridgeBeerlistCurrentComponent } from './components/fridge-beerlist-current/fridge-beerlist-current.component';
import { FridgeBeerlistHistoryComponent } from './components/fridge-beerlist-history/fridge-beerlist-history.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { TransferCreditComponent } from './components/transfer-credit/transfer-credit.component';
import { TakeBeerComponent } from './components/take-beer/take-beer.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatListModule} from '@angular/material/list';

export function initializeApp(appConfig: AppConfig) {
  const promise = appConfig.loadAppConfig().pipe(tap((settings: IAppConfig) => {
    const protectedResourceMap:[string, string[]][]=[[settings.apiUrl, [settings.scopeUrl]] ];

    msalConfig = {
      authority: settings.authority,
      clientID: settings.clientId,
      protectedResourceMap: protectedResourceMap,
      consentScopes: [ settings.scope, settings.scopeUrl ]
    };
  })).toPromise();
  return () => promise;
}

let msalConfig: MsalConfig; 

export function msalConfigFactory() {
  return msalConfig;
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AddBeerComponent,
    ViewCreditComponent,
    FridgeBeerlistCurrentComponent,
    FridgeBeerlistHistoryComponent,
    TransferCreditComponent,
    TakeBeerComponent,
  ],
  imports: [
    MsalModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatMenuModule,
    MatIconModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatButtonModule,
    MatListModule
  ],
  providers: [ 
    BeerService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [AppConfig],
      multi: true
    },
    MsalService,
    {
      provide: MSAL_CONFIG,
      useFactory: msalConfigFactory
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }

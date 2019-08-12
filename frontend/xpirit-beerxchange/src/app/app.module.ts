import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MsalModule, MsalInterceptor, MsalConfig } from '@azure/msal-angular';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BeerService } from './beer.service';
import { AppConfig } from './app.config';
import { MSAL_CONFIG, MsalService } from "@azure/msal-angular/dist/msal.service";
import { config } from 'rxjs';

export const protectedResourceMap:[string, string[]][]=[['https://localhost:44388/api/values', ['api://e8c5f737-0341-4da1-bcd0-3cec89e8ea11/BeerAccess']] ];

export function initializeApp(appConfig: AppConfig) {
  return () => appConfig.load();
}


export function getConfig() {
  var msalConfig = new MsalConfig();
  msalConfig.authority = 'https://login.microsoftonline.com/3d4d17ea-1ae4-4705-947e-51369c5a5f79';
  msalConfig.clientID = '935fb522-2d55-4ee5-a6a8-6a57d9c33b73';
  msalConfig.protectedResourceMap = protectedResourceMap;
  msalConfig.consentScopes = [ "user.read", "api://e8c5f737-0341-4da1-bcd0-3cec89e8ea11/BeerAccess" ];
  
  return msalConfig;
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    MsalModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
  ],
  providers: [ 
    BeerService,
    AppConfig,
    { 
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [AppConfig], multi: true 
    },
    MsalService,
    {
      provide: MSAL_CONFIG,
      useValue: getConfig()
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

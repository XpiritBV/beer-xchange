import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MsalModule, MsalInterceptor } from '@azure/msal-angular';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BeerService } from './beer.service';

export const protectedResourceMap:[string, string[]][]=[['https://localhost:44388/api/values', ['api://e8c5f737-0341-4da1-bcd0-3cec89e8ea11/BeerAccess']] ];

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    MsalModule.forRoot({ 
      clientID: '935fb522-2d55-4ee5-a6a8-6a57d9c33b73',
      authority: 'https://login.microsoftonline.com/3d4d17ea-1ae4-4705-947e-51369c5a5f79',
      consentScopes: [ "user.read", "api://e8c5f737-0341-4da1-bcd0-3cec89e8ea11/BeerAccess" ],
      protectedResourceMap: protectedResourceMap
    }),
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
  ],
  providers: [ BeerService, {
    provide: HTTP_INTERCEPTORS,
    useClass: MsalInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})

export class AppModule { }

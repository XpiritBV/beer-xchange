import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { MsalGuard } from '@azure/msal-angular';
import { HomeComponent } from './components/home/home.component';
import { AddBeerComponent } from './components/add-beer/add-beer.component';
import { ViewCreditComponent } from './components/view-credit/view-credit.component';
import { TransferCreditComponent } from './components/transfer-credit/transfer-credit.component';
import { TakeBeerComponent } from './components/take-beer/take-beer.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [MsalGuard] },
  { path: 'add', component: AddBeerComponent, canActivate: [MsalGuard] },
  { path: 'credit', component: ViewCreditComponent, canActivate: [MsalGuard] },
  { path: 'take', component: TakeBeerComponent, canActivate: [MsalGuard] },
  { path: 'transfer', component: TransferCreditComponent, canActivate: [MsalGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{ useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

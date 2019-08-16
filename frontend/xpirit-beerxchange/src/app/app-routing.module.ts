import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { MsalGuard } from '@azure/msal-angular';
import { HomeComponent } from './components/home/home.component';
import { AddBeerComponent } from './components/add-beer/add-beer.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [MsalGuard] },
  { path: 'add', component: AddBeerComponent, canActivate: [MsalGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

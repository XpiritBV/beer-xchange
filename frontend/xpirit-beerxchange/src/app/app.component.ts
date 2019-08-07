import { Component } from '@angular/core';
import { BeerService } from './beer.service';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'xpirit-beerxchange';

  constructor(private beerService : BeerService, private msal: MsalService) { }

  user : any;

  ngOnInit() {
    var a = this.beerService.getBeers();

    this.user = this.msal.getUser()
  }
}

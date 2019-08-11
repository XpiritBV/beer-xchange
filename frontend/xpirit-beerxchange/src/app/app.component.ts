import { Component } from '@angular/core';
import { BeerService } from './beer.service';
import { MsalService } from '@azure/msal-angular';
import { AppConfig } from './app.config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'xpirit-beerxchange';
  test: string = 'Test';

  constructor(private beerService : BeerService, private msal: MsalService) { }

  user : any;

  ngOnInit() {
    var a = this.beerService.getBeers();

    this.user = this.msal.getUser()

    AppConfig.settings.subscribe((settings) => {
      this.test = settings.apiUrl;
    });
  }
}

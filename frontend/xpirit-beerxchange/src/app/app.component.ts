import { Component } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { AppConfig } from './app.config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Xpirit Beer XChange';

  constructor(private msal: MsalService) { }

  user : any;

  ngOnInit() {

    this.user = this.msal.getUser()
  }
}

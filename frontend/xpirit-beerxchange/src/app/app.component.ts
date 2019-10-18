import { Component, ViewChild } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { MatSidenav } from '@angular/material';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Xpirit Beer XChange';
  user : any;

  constructor(private msal: MsalService) { }

  ngOnInit() {
    this.user = this.msal.getUser();
  }
}

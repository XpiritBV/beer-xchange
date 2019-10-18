import { Component, OnInit, OnDestroy } from '@angular/core';
import { FridgeService } from '../../services/fridge.service';
import { Subscription } from 'rxjs';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-view-credit',
  templateUrl: './view-credit.component.html',
  styleUrls: ['./view-credit.component.css']
})
export class ViewCreditComponent implements OnInit, OnDestroy {
  private readonly _subscription: Subscription = new Subscription();
  credits: number;

  constructor(private fridgeService: FridgeService,
              private msal: MsalService) { }

  ngOnInit() {
    const user = this.msal.getUser();

    this._subscription.add(
        this.fridgeService.getCreditsForCurrentUser(user.name).subscribe((credits: number) => {
            this.credits = credits;
        })
    );
  }
  ngOnDestroy() {
    this._subscription.unsubscribe();
  }
}

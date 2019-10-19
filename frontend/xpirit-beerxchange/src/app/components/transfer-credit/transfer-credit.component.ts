import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FridgeService } from 'src/app/services/fridge.service';
import { Beer } from 'src/app/model/beer';
import { Router } from '@angular/router';
import { CreditTransfer } from '../../model/creditTransfer';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-transfer-credit',
  templateUrl: './transfer-credit.component.html',
  styleUrls: ['./transfer-credit.component.css']
})
export class TransferCreditComponent implements OnInit {

  angForm: FormGroup;
  constructor(private fb: FormBuilder, private fridgeService: FridgeService, private router: Router, private msal: MsalService) {
    this.createForm();
  }
  
  users: Array<string> = [];
  beers: Array<Beer> = [];
  selectedBeer: number;
  
  createForm() {
    this.angForm = this.fb.group({
      credit_receiver: [null, Validators.required ],
      beer_id: [null,Validators.required]
    });
  }
  
  ngOnInit() {
    var currentUser = this.msal.getUser();

    this.fridgeService.getFridgeUsers().subscribe((users: Array<string>) => {
      this.users = users;
    });
    
    this.fridgeService.getUserBeers(currentUser.name).subscribe((beers: Array<Beer>) => {
      this.beers = beers;  
    });
  }

  onSubmit(){
    if(this.selectedBeer == -1)
    {
      this.selectedBeer = null;
    }
    
    var creditTransfer = {
      creditReceiver: this.angForm.value.credit_receiver,
      beerId: this.angForm.value.beer_id,} as CreditTransfer;
    console.log(creditTransfer);

    this.fridgeService.transferCredit(creditTransfer).subscribe();
    
    this.router.navigate(["/"]);
  }

}

import { Component, OnInit } from '@angular/core';
import { FormGroup,  FormBuilder,  Validators, NgForm } from '@angular/forms';
import { FridgeService } from 'src/app/services/fridge.service';
import { Beer } from 'src/app/model/beer';
import { Router } from '@angular/router';
import { BeerAddition } from 'src/app/model/beerAddition';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-add-beer',
  templateUrl: './add-beer.component.html',
  styleUrls: ['./add-beer.component.css']
})
export class AddBeerComponent implements OnInit {
  
  angForm: FormGroup;
  constructor(private fb: FormBuilder, private fridgeService: FridgeService, private router: Router, private msal: MsalService) {
    this.createForm();
  }
  
  users: Array<string> = [];
  switchedForBeers: Array<Beer> = [];
  selectedBeer: number;
  
  createForm() {
    this.angForm = this.fb.group({
      beer_name: ['', Validators.required ],
      brewery: ['', Validators.required ],
      country: ['', Validators.required ],
      added_by: [null, Validators.required ],
      switched_for_beer: [null,Validators.required]
    });
  }
  
  ngOnInit() {
    this.fridgeService.getFridgeUsers().subscribe((users: Array<string>) => {
      var currentUser = this.msal.getUser();
      if(!users.find(u =>u == currentUser.name))
      {
        users.unshift(currentUser.name);
      }
      this.users = users;
    });
    
    this.fridgeService.getCurrentBeers().subscribe((beers: Array<Beer>) => {
      const noBeer = <Beer> {
        name: "i do not want to take a beer at this moment",
        id: -1
      }
      beers.unshift(noBeer);
      this.switchedForBeers = beers;  
      this.selectedBeer = noBeer.id;
    });
  }

  onSubmit(){
    if(this.selectedBeer == -1) {
      this.selectedBeer = null;
    }
    
    var beer = {
      beerName: this.angForm.value.beer_name,
      brewery: this.angForm.value.brewery,
      country: this.angForm.value.country,
      addedBy: this.angForm.value.added_by,
      switchedBeer: this.angForm.value.switched_for_beer,
    } as BeerAddition;

    this.fridgeService.addBeer(beer).subscribe(() => {
      this.router.navigate(["/"]);
    },(err) => {
      // TODO: Nette foutmelding tonen
      alert('Something went wrong');
    })
  }
}

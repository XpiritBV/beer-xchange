import { Component, OnInit } from '@angular/core';
import { FormGroup,  FormBuilder,  Validators, NgForm } from '@angular/forms';
import { FridgeService } from 'src/app/services/fridge.service';
import { Beer } from 'src/app/model/beer';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-beer',
  templateUrl: './add-beer.component.html',
  styleUrls: ['./add-beer.component.css']
})
export class AddBeerComponent implements OnInit {
  
  angForm: FormGroup;
  constructor(private fb: FormBuilder, private fridgeService: FridgeService, private router: Router) {
    this.createForm();
  }
  
  users: Array<string> = [];
  switchedForBeers: Array<Beer> = [];
  beerName :string;
  brewery:string;
  country:string;
  addedBy : string;
  selectedBeer: number;
  
  createForm() {
    this.angForm = this.fb.group({
      beer_name: ['', Validators.required ],
      brewery: ['', Validators.required ],
      country: ['', Validators.required ],
      added_by: ['', Validators.required ],
      switched_for_beer: ['',Validators.required]
    });
  }
  
  ngOnInit() {
    this.fridgeService.getFridgeUsers().subscribe((users: Array<string>) => {
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
    if(this.selectedBeer == -1)
    {
      this.selectedBeer = null;
    }
    var beer = {name: this.beerName,
                   brewery: this.brewery,
                   country: this.country,
                   createdBy: this.addedBy,
                   switchedForId: this.selectedBeer,
                   id: 0,
                   picture: '',
                   addedDate: null,
                   removedDate: null,
                   removedBy: null
                   } as Beer;
    this.fridgeService.addBeer(beer);
    this.router.navigate(["/"]);
  }
}

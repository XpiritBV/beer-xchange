import { Component, OnInit } from '@angular/core';
import { FridgeService } from 'src/app/services/fridge.service';
import { Beer } from 'src/app/model/beer';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { BeerRemoval } from 'src/app/model/beerRemoval';
import { Router } from '@angular/router';

@Component({
  selector: 'app-take-beer',
  templateUrl: './take-beer.component.html',
  styleUrls: ['./take-beer.component.css']
})
export class TakeBeerComponent implements OnInit {
  beers: Array<Beer> = [];
  angForm: FormGroup;

  constructor(private fridgeService: FridgeService, private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.fridgeService.getCurrentBeers().subscribe((beers: Array<Beer>) => {
      this.beers = beers.sort((x,y) => x.name > y.name ? 1 : -1);
    });

    this.angForm = this.fb.group({
      selectedBeer: ['', Validators.required ]
    });
  }

  onSubmit(): void {
    var beerRemoval = {beerId: this.angForm.value.selectedBeer
      } as BeerRemoval;
    
      this.fridgeService.takeBeer(beerRemoval).subscribe(() => {
      this.router.navigate(["/"]);
    },(err) => {
      // TODO: Nette foutmelding tonen
      alert('Something went wrong');
    })
  }
}

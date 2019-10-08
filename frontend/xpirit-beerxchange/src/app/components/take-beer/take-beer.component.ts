import { Component, OnInit } from '@angular/core';
import { FridgeService } from 'src/app/services/fridge.service';
import { Beer } from 'src/app/model/beer';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-take-beer',
  templateUrl: './take-beer.component.html',
  styleUrls: ['./take-beer.component.css']
})
export class TakeBeerComponent implements OnInit {
  beers: Array<Beer> = [];
  angForm: FormGroup;

  constructor(private fridgeService: FridgeService, private fb: FormBuilder) { }

  ngOnInit() {
    this.fridgeService.getCurrentBeers().subscribe((beers: Array<Beer>) => {
      this.beers = beers;
    });

    this.angForm = this.fb.group({
      selectedBeer: ['', Validators.required ]
    });
  }

  onSubmit(): void {
    this.fridgeService.takeBeer(this.angForm.value.selectedBeer).subscribe()
  }
}

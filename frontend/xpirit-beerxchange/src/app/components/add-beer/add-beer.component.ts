import { Component, OnInit } from '@angular/core';
import { FormGroup,  FormBuilder,  Validators } from '@angular/forms';
import { FridgeService } from 'src/app/services/fridge.service';

@Component({
  selector: 'app-add-beer',
  templateUrl: './add-beer.component.html',
  styleUrls: ['./add-beer.component.css']
})
export class AddBeerComponent implements OnInit {

  angForm: FormGroup;
  constructor(private fb: FormBuilder, private fridgeService: FridgeService) {
    this.createForm();
  }

  users: Array<string> = [];

  createForm() {
    this.angForm = this.fb.group({
      beer_name: ['', Validators.required ],
      brewery: ['', Validators.required ],
      country: ['', Validators.required ],
      added_by: ['', Validators.required ]
    });
  }

  ngOnInit() {
    this.fridgeService.getFridgeUsers().subscribe((users: Array<string>) => {
      this.users = users;
    });
  }


}

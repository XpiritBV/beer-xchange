import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBeerComponent } from './add-beer.component';

describe('AddBeerComponent', () => {
  let component: AddBeerComponent;
  let fixture: ComponentFixture<AddBeerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddBeerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddBeerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

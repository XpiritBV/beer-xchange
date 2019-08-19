import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FridgeBeerlistCurrentComponent } from './fridge-beerlist-current.component';

describe('FridgeBeerlistCurrentComponent', () => {
  let component: FridgeBeerlistCurrentComponent;
  let fixture: ComponentFixture<FridgeBeerlistCurrentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FridgeBeerlistCurrentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FridgeBeerlistCurrentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

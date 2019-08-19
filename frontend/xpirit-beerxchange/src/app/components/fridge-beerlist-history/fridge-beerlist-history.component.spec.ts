import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FridgeBeerlistHistoryComponent } from './fridge-beerlist-history.component';

describe('FridgeBeerlistHistoryComponent', () => {
  let component: FridgeBeerlistHistoryComponent;
  let fixture: ComponentFixture<FridgeBeerlistHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FridgeBeerlistHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FridgeBeerlistHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

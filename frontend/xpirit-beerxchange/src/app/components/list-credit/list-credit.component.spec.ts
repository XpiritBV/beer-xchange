import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCreditComponent } from './list-credit.component';

describe('ListCreditComponent', () => {
  let component: ListCreditComponent;
  let fixture: ComponentFixture<ListCreditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListCreditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListCreditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

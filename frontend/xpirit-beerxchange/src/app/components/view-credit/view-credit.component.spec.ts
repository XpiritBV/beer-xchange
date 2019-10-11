import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewCreditComponent } from './view-credit.component';

describe('ViewCreditComponent', () => {
  let component: ViewCreditComponent;
  let fixture: ComponentFixture<ViewCreditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewCreditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewCreditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

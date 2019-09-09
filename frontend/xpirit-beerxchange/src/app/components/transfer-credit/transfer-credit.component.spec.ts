import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferCreditComponent } from './transfer-credit.component';

describe('TransferCreditComponent', () => {
  let component: TransferCreditComponent;
  let fixture: ComponentFixture<TransferCreditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TransferCreditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TransferCreditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

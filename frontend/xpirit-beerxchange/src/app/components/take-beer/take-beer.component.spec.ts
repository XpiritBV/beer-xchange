import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TakeBeerComponent } from './take-beer.component';

describe('TakeBeerComponent', () => {
  let component: TakeBeerComponent;
  let fixture: ComponentFixture<TakeBeerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TakeBeerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TakeBeerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

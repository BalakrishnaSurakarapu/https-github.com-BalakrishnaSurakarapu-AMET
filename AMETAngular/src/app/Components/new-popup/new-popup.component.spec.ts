import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewPopupComponent } from './new-popup.component';

describe('NewPopupComponent', () => {
  let component: NewPopupComponent;
  let fixture: ComponentFixture<NewPopupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewPopupComponent]
    });
    fixture = TestBed.createComponent(NewPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

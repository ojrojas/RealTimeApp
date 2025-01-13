import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsermessageComponent } from './usermessage.component';

describe('UsermessageComponent', () => {
  let component: UsermessageComponent;
  let fixture: ComponentFixture<UsermessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UsermessageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsermessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserandtimeComponent } from './userandtime.component';

describe('UserandtimeComponent', () => {
  let component: UserandtimeComponent;
  let fixture: ComponentFixture<UserandtimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserandtimeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserandtimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListUsersConnectedComponent } from './list-users-connected.component';

describe('ListUsersConnectedComponent', () => {
  let component: ListUsersConnectedComponent;
  let fixture: ComponentFixture<ListUsersConnectedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListUsersConnectedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListUsersConnectedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

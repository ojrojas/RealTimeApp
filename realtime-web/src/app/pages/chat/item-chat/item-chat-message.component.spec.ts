import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemChatComponent } from './item-chat-message.component';

describe('ItemChatComponent', () => {
  let component: ItemChatComponent;
  let fixture: ComponentFixture<ItemChatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ItemChatComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ItemChatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

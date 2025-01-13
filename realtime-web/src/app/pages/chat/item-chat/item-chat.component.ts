import { Component, Input } from '@angular/core';
import { UsermessageComponent } from "./usermessage/usermessage.component";
import { UserandtimeComponent } from './userandtime/userandtime.component';
import { MatCardModule } from '@angular/material/card';
import { IChat } from '../../../core/models/chat.model';

@Component({
  selector: 'app-item-chat',
  imports: [
    UserandtimeComponent,
    UsermessageComponent,
    MatCardModule
  ],
  templateUrl: './item-chat.component.html',
  styleUrl: './item-chat.component.css'
})
export class ItemChatComponent {
  @Input() chat: IChat | undefined;

  constructor() {
  }
}

import { Component, Input } from '@angular/core';
import { UsermessageComponent } from "./usermessage/usermessage.component";
import { UserandtimeComponent } from './userandtime/userandtime.component';
import { MatCardModule } from '@angular/material/card';
import { IMessage } from '../../../core/models/message.model';

@Component({
  selector: 'app-item-chat',
  imports: [
    UserandtimeComponent,
    UsermessageComponent,
    MatCardModule
  ],
  templateUrl: './item-chat-message.component.html',
  styleUrl: './item-chat-message.component.css'
})
export class ItemChatComponent {
  @Input() message: IMessage | undefined;
}

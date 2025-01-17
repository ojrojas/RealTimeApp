import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { ChatStore } from '../../../core/stores/chat.store';
import { IChat } from '../../../core/models/chat.model';

@Component({
  selector: 'app-list-chats',
  imports: [
    MatDividerModule,
    MatListModule,
    MatIconModule,
    CommonModule,
    MatCardModule
  ],
  templateUrl: './list-chats.component.html',
  styleUrl: './list-chats.component.css'
})
export class ListChatsComponent {
  readonly chatStore = inject(ChatStore);

  onSelected = (chat: IChat) => {
    console.log(chat.name, chat.id, chat.users);
  }
}

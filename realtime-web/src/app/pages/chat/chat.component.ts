import { Component, inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ItemChatComponent } from './item-chat/item-chat-message.component';
import { HubconnectionService } from '../../core/services/hubconnection.service';
import { FormMessageComponent } from "./form-message/form-message.component";
import { ChatStore } from '../../core/stores/chat.store';
import { ListUsersConnectedComponent } from "./list-users-connected/list-users-connected.component";
import { UserStore } from '../../core/stores/identity.store';
import { Router } from '@angular/router';
import { ListChatsComponent } from "./list-chats/list-chats.component";


@Component({
  selector: 'app-chat',
  imports: [
    MatFormFieldModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDividerModule,
    MatFormFieldModule,
    MatIconModule,
    ItemChatComponent,
    FormMessageComponent,
    ListUsersConnectedComponent,
    ListChatsComponent
],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent {
  readonly hubService = inject(HubconnectionService);
  readonly userStore = inject(UserStore);
  readonly chatStore = inject(ChatStore);
  readonly router = inject(Router);

  constructor() {
    this.hubService.statusConnetionHub();
    this.userStore.getUserInfo();
  }

  onLogout = () => {
    this.userStore.logout();
    this.hubService.disconnect();
    this.router.navigate(['/login']);
  }
}

import { CommonModule } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ChatStore } from '../../../core/stores/chat.store';
import { IChat } from '../../../core/models/chat.model';
import { UserStore } from '../../../core/stores/identity.store';
import { IApplicationUser } from '../../../core/models/applicationuser.model';

@Component({
  selector: 'app-form-message',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './form-message.component.html',
  styleUrl: './form-message.component.css'
})
export class FormMessageComponent {
  @Input() userReceiver : IApplicationUser | undefined;

  readonly chatStore = inject(ChatStore);
  readonly userStore = inject(UserStore);

  formChatGroup = new FormGroup({
    input: new FormControl("", Validators.required)
  });

  constructor(){
    this.userStore.getUserInfo();
  }

  onSendMessage = (message: string | null) => {
    const chat: IChat = {
      comunicateType : 1,
      dateTimeOffset : new Date(),
      userAnnouncer: this.userStore.user()?.id!,
      message: message!,
      nameAnnouncer: this.userReceiver?.name!,
      receiver: this.userReceiver?.id!,
      nameReceiver: this.userReceiver?.name!
    };

    this.chatStore.createChat(chat);
  }

  getDisableButton = () => {
    if (!this.formChatGroup.valid)
      return true;
    else return false;
  }
}

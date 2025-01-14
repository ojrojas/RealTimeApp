import { CommonModule } from '@angular/common';
import { Component, inject, isDevMode } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ChatStore } from '../../../core/stores/chat.store';
import { UserStore } from '../../../core/stores/identity.store';
import { IApplicationUser } from '../../../core/models/applicationuser.model';
import { IChatMessageRequest } from '../../../core/dtos/chatmessage.request.dto';
import { MatSnackBar } from '@angular/material/snack-bar';


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
  readonly chatStore = inject(ChatStore);
  readonly userStore = inject(UserStore);
  readonly snackBar = inject(MatSnackBar);

  formChatGroup = new FormGroup({
    input: new FormControl("", Validators.required)
  });

  onSendMessage = (message: string | null) => {
    if (this.formChatGroup.valid) {
      const chat: IChatMessageRequest = {
        message: message!,
        receiver: this.userStore.userSelected()?.id!
      };

      this.chatStore.createChat(chat);
      this.chatStore.getChats(this.userStore.userSelected()?.id!);
    } else this.snackBar.open("Error message input not valid");
  }

  getDisableButton = () => {
    if (isDevMode())
      console.log("validating message text")
    if (!this.formChatGroup.valid || this.userStore.userSelected() === undefined)
      return true;
    else return false;
  }

  convertCompleteName = (user: IApplicationUser) => {
    return user.name + " " + user.lastName;
  }
}

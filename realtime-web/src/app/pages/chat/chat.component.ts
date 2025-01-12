import { Component, inject, isDevMode } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { CommonModule } from '@angular/common';
import { UserStore } from '../../core/stores/identity.store';
import { ChatService } from '../../core/services/chat-service.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';


@Component({
  selector: 'app-chat',
  imports: [
    MatFormFieldModule,
    FormsModule,
     CommonModule,
        ReactiveFormsModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent {

  messages='';
  formChatGroup = new FormGroup({
    input: new FormControl("", Validators.required)
  })
  connection: signalR.HubConnection;
  store = inject(UserStore);
  service = inject(ChatService);

  constructor() {
    if (isDevMode())
      console.log("token ", this.store.access_token()?.tokenAccess);
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(environment.api + "/hub-connect", {
        accessTokenFactory: () => {
          return `${this.store.access_token()?.tokenAccess!}`
        },
        transport: signalR.HttpTransportType.LongPolling
      })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Debug).build();
    this.connection.on("SendMessageAsync", (message) => this.onReceiveMessage(message));
    this.connection.start().catch(this.logErrors);
  }

  logErrors = (err: string) => {
    console.error("errors signal", err);
  }

  onReceiveMessage = (message: string) => {
    console.log(message);
    this.messages += "\n" + message;
  }

  onSendMessage = (message: string | null) => {
    console.log("Send message -----> ", message);
    this.connection.invoke("SendMessage", this.connection.connectionId, message).then(res => {
      let input = document.getElementById("toSend") as HTMLInputElement;
      input.value = "";
    });
  }

  statusConnetionHub = () => {
    if (isDevMode())
      console.log("hubbuilder", this.connection);
  }

}

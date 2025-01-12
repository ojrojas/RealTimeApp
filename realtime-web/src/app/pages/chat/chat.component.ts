import { Component, inject, isDevMode } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { CommonModule } from '@angular/common';
import { UserStore } from '../../core/stores/identity.store';
import { ChatService } from '../../core/services/chat-service.service';

@Component({
  selector: 'app-chat',
  imports: [],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
  providers: [CommonModule]
})
export class ChatComponent {
  messages = '';
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

  onSendMessage = (message: string) => {
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

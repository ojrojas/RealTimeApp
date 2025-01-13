import { inject, Injectable, isDevMode, signal } from '@angular/core';
import { UserStore } from '../stores/identity.store';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HubconnectionService {
  connection: signalR.HubConnection;
  store = inject(UserStore);
  stateConnection = signal('');

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
    this.connection.on("SendMessageAsync", (message) => this.onReceiveMessage());
    this.connection.start().catch(this.logErrors);
  }

  logErrors = (err: string) => {
    console.error("errors signal", err);
  }

  onReceiveMessage = () => {
  }

  statusConnetionHub = () => {
    if (isDevMode())
      console.log("hubbuilder", this.connection.state === 'Connected');
    return this.connection.state === 'Connected';
  }

  onSendMessage = (message: string | null) => {
    if (isDevMode())
      console.log("Send message -----> ", message);
    this.connection.invoke("SendMessage", this.connection.connectionId, message).then(res => {
      let input = document.getElementById("toSend") as HTMLInputElement;
      input.value = "";
    });
  }
}

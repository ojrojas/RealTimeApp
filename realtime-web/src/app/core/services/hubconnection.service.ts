import { inject, Injectable, isDevMode, signal } from '@angular/core';
import { UserStore } from '../stores/identity.store';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import {MatSnackBar} from '@angular/material/snack-bar';
import { ChatStore } from '../stores/chat.store';

@Injectable({
  providedIn: 'root'
})
export class HubconnectionService {
  readonly connection: signalR.HubConnection;
  readonly store = inject(UserStore);
  readonly snakBar = inject(MatSnackBar);
  readonly chatStore = inject(ChatStore);

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
    this.connection.on("SendMessageAsync", (message) => this.onSendMessage(message));
    this.connection.on("SendNotificationAsync", (title,message) => this.onSendNotification(title, message));
    this.connection.on("SendAllNotificationAsync", (title, message) => this.onSendAllNotification(title, message));
    this.connection.on("NotificationConnectionUserAsync", () => this.onNotificationConnectionUser());

    this.connection.start().then(response => {
      this.store.getUserInfo();
      this.invokeOnNotificationConnectionUser();
    }).catch(this.logErrors);
  }

  logErrors = (err: string) => {
    console.error("errors signal", err);
  }

  onSendAllNotification = (title:string, message:string) => {
    this.snakBar.open(message, title, {duration: 5000});
  }

  onSendNotification = (title:string, message:string) => {
    this.snakBar.open(message, title, {duration: 5000});
  }

  onNotificationConnectionUser =() => {
    this.store.getUsersConnected();

    this.snakBar.open("New user connected", "Info", {duration: 5000});
  }

  onSendMessage = (message: string | null) => {
    if (isDevMode())
      console.log("Send message -----> ", message);
    this.connection.invoke("SendMessage", this.connection.connectionId, message).then(res => {
      let input = document.getElementById("toSend") as HTMLInputElement;
      input.value = "";
    });
  }

  statusConnetionHub = () => {
    if (isDevMode())
      console.log("hubbuilder", this.connection.state === 'Connected');
    return this.connection.state === 'Connected';
  }

  invokeOnNotificationConnectionUser = () =>{
    this.connection.invoke("NotificationConnectionUser");
    this.chatStore.getlistChats();
  }

  disconnect = () => {
    this.connection.stop();
  }
}

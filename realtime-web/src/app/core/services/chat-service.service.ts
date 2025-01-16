import { HttpClient, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { IChat } from '../models/chat.model';
import { Observable } from 'rxjs';
import { UserStore } from '../stores/identity.store';
import { IChatMessageRequest } from '../dtos/chatmessages.request.dto';
import { IChatMessagesResponse } from '../dtos/chatmessages.response.dto';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  http = inject(HttpClient);
  store = inject(UserStore);
  constructor() { }

  createMessageChat = (chat: IChatMessageRequest) : Observable<HttpResponse<any>> => {
    return this.http.post(
      environment.api.concat("/chat"),
      chat,
      {
        headers: { ["Authorization"]: `Bearer ${this.store.access_token()?.tokenAccess}` },
        observe: 'response'
      });
  }

  listChatMessages = (): Observable<HttpResponse<IChatMessagesResponse>> => {
    return this.http.get<IChatMessagesResponse>(environment.api.concat(`/listchatmessages`),  {
      headers: { ["Authorization"]: `Bearer ${this.store.access_token()?.tokenAccess}` },
      observe: 'response'
    });
  }

  listChats = (): Observable<HttpResponse<IChat[]>> => {
    return this.http.get<IChat[]>(environment.api.concat(`/listchats`),  {
      headers: { ["Authorization"]: `Bearer ${this.store.access_token()?.tokenAccess}` },
      observe: 'response'
    });
  }

}

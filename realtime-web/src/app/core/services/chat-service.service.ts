import { HttpClient, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { IChat } from '../models/chat.model';
import { Observable } from 'rxjs';
import { UserStore } from '../stores/identity.store';
import { IChatMessageRequest } from '../dtos/chatmessage.request.dto';

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

  listChat = (id:string): Observable<HttpResponse<IChat[]>> => {
    return this.http.get<IChat[]>(environment.api.concat(`/chat/${id}`),  {
      headers: { ["Authorization"]: `Bearer ${this.store.access_token()?.tokenAccess}` },
      observe: 'response'
    });
  }

}

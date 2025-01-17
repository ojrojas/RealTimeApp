import { inject, signal } from "@angular/core";
import { IApplicationUser } from "../models/applicationuser.model";
import { IChat } from "../models/chat.model";
import { patchState, signalStore, withMethods, withState } from "@ngrx/signals";
import { setFulfilled, setPending, withRequestStatus } from "./request-status.store";
import { ChatService } from "../services/chat-service.service";
import { IChatMessageRequest } from "../dtos/chatmessages.request.dto";
import { IChatMessagesResponse } from "../dtos/chatmessages.response.dto";
import { IChatsResponse } from "../dtos/chats-response.dtos";

type ChatState = {
  chatMessageResponse: IChatMessagesResponse | null;
  chats: IChatsResponse | null;
  chatSelected: IChat | null;
  usersConnected: IApplicationUser | null;
}

const chatState = signal<ChatState>({
  usersConnected: null,
  chatMessageResponse: null,
  chatSelected: null,
  chats: null
});

export const ChatStore = signalStore(
  { providedIn: 'root' },
  withState(chatState),
  withRequestStatus(),
  withMethods((store, service = inject(ChatService)) => ({
    createChat(chat: IChatMessageRequest) {
      patchState(store, setPending());
      service.createMessageChat(chat).subscribe({
        next: response => {
          patchState(store, setFulfilled());
        },
        error: (err) => console.error(err)
      })
    },
    getlistChatMessages() {
      patchState(store, setPending);
      console.log("request get chats");
      service.listChatMessages().subscribe({
        next: response => {
          patchState(store, {chatMessageResponse: response.body as IChatMessagesResponse}, setFulfilled());
          console.log("chatmessages got it ", response.body);
        },
        error: (err) => console.error(err)
      })},
      getlistChats() {
        patchState(store, setPending);
        console.log("request get chats");
        service.listChats().subscribe({
          next: response => {
            patchState(store, {chats: response.body as IChatsResponse}, setFulfilled());
          },
          error: (err) => console.error(err)
        })}
  }))
);

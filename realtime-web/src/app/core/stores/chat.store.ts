import { inject, signal } from "@angular/core";
import { IApplicationUser } from "../models/applicationuser.model";
import { IChat } from "../models/chat.model";
import { patchState, signalStore, withMethods, withState } from "@ngrx/signals";
import { setFulfilled, setPending, withRequestStatus } from "./request-status.store";
import { ChatService } from "../services/chat-service.service";

type ChatState = {
  chats: IChat[] | null;
  usersConnected: IApplicationUser | null;
}

const chatState = signal<ChatState>({
  usersConnected: null,
  chats: null
});

export const ChatStore = signalStore(
  { providedIn: 'root' },
  withState(chatState),
  withRequestStatus(),
  withMethods((store, service = inject(ChatService)) => ({
    createChat(chat: IChat) {
      patchState(store, setPending());
      service.createMessageChat(chat).subscribe({
        next: response => {
          patchState(store, setFulfilled());
        },
        error: (err) => console.error(err)
      })
    },
    getChats(id:string) {
      patchState(store, setPending);
      service.listChat(id).subscribe({
        next: response => {
          patchState(store, {chats: response.body}, setFulfilled());
        },
        error: (err) => console.error(err)
      })
    }
  }))
);

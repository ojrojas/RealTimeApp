import { IMessage } from "./message.model";

export interface IChat {
  id: string;
  name: string;
  chatDate: Date;
  users: string[];
  messages: IMessage[];
}

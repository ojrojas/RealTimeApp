export interface IMessage {
  id: string;
  messageDate: Date;
  messageWrited: string;
  writerUserId: string;
  isReadMessage: boolean;
  nameWriter: string;
  chatId: string;
  attachment: string;
}

export interface IChat {
  id?: string;
  userAnnouncer: string;
  nameAnnouncer:string;
  receiver: string;
  nameReceiver: string;
  dateTimeOffset: Date;
  message: string;
  isReadMessage?: boolean;
  comunicateType:number
}

export enum ComunicateType {
  announcer = 1,
  receiver
}

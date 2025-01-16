import { Component, Input } from '@angular/core';
import { IMessage } from '../../../../core/models/message.model';

@Component({
  selector: 'app-usermessage',
  imports: [],
  templateUrl: './usermessage.component.html',
  styleUrl: './usermessage.component.css'
})
export class UsermessageComponent {
  @Input() message: IMessage | undefined;
}

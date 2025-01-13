import { Component, Input } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import { IChat } from '../../../../core/models/chat.model';

@Component({
  selector: 'app-userandtime',
  imports: [MatCardModule,MatIconModule],
  templateUrl: './userandtime.component.html',
  styleUrl: './userandtime.component.css'
})
export class UserandtimeComponent {
  @Input() item: IChat | undefined;
  announcer = 1;

  constructor(){
  }

  getComunicateType = (item:IChat) => {
      return item.comunicateType
  }

  getNameUserCommunicate = (item:IChat)=>{
    if(item.comunicateType === this.announcer)
      return item.nameAnnouncer;
    else return item.nameReceiver;
  }
}

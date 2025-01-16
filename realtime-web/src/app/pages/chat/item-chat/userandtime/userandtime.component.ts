import { Component, Input } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import { IMessage } from '../../../../core/models/message.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-userandtime',
  imports: [MatCardModule,MatIconModule, DatePipe],
  templateUrl: './userandtime.component.html',
  styleUrl: './userandtime.component.css'
})
export class UserandtimeComponent {
  @Input() message: IMessage | undefined;
}

import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import * as signalR from '@microsoft/signalr';
import { environment } from './../environments/environment';




@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'realtime-web';


}

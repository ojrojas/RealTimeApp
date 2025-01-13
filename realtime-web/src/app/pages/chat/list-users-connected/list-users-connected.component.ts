import { Component, inject } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { UserStore } from '../../../core/stores/identity.store';
import { IApplicationUser } from '../../../core/models/applicationuser.model';


@Component({
  selector: 'app-list-users-connected',
  imports: [
    MatDividerModule,
    MatListModule
  ],
  templateUrl: './list-users-connected.component.html',
  styleUrl: './list-users-connected.component.css'
})
export class ListUsersConnectedComponent {
  userStore = inject(UserStore);
  constructor(){
    this.userStore.getUsersConnected();
  }
}

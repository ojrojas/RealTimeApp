import { Component, inject } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { UserStore } from '../../../core/stores/identity.store';
import { MatIconModule } from '@angular/material/icon';
import { IApplicationUser } from '../../../core/models/applicationuser.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-list-users-connected',
  imports: [
    MatDividerModule,
    MatListModule,
    MatIconModule,
    CommonModule
  ],
  templateUrl: './list-users-connected.component.html',
  styleUrl: './list-users-connected.component.css'
})
export class ListUsersConnectedComponent {
  userStore = inject(UserStore);
  constructor() {
  }

  onSelected = (userSelected: IApplicationUser) => {
    this.userStore.setUserSelected(userSelected);
  }
}

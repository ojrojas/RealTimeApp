import { Routes } from '@angular/router';
import { ChatComponent } from './pages/chat/chat.component';
import { authGuard } from './core/auth/auth.guard';
import { LoginComponent } from './pages/login/login.component';
import { ItemChatComponent } from './pages/chat/item-chat/item-chat.component';

export const routes: Routes = [
  {
    path: 'chat',
    component: ChatComponent,
    canActivate: [authGuard]
  },
  {
    path:'item-chat',
    component: ItemChatComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path:'',
     redirectTo: 'chat',
   // redirectTo: 'item-chat',
    pathMatch: "full"
  },
  {
    path: '**',
     redirectTo: 'chat',
    //redirectTo: 'item-chat',
  }
];

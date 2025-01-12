import { Routes } from '@angular/router';
import { ChatComponent } from './pages/chat/chat.component';
import { authGuard } from './core/auth/auth.guard';
import { LoginComponent } from './pages/login/login.component';

export const routes: Routes = [
  {
    path: 'chat',
    component: ChatComponent,
    canActivate: [authGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path:'',
    redirectTo: 'chat',
    pathMatch: "full"
  },
  {
    path: '**',
    redirectTo: 'chat',
  }
];

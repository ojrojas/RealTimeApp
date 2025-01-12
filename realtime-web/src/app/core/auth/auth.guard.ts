import { inject, isDevMode } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserStore } from '../stores/identity.store';

export const authGuard: CanActivateFn = (route, state) => {
  const store = inject(UserStore);
  if (isDevMode())
    console.log("store", store);
  const router = inject(Router);
  if (store.access_token()?.tokenAccess === undefined) {
    router.navigate(["/login"]);
  }
  return true;
};

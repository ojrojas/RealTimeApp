import { effect, signal } from "@angular/core";
import { getState, patchState, signalStore, withHooks, withMethods, withState } from "@ngrx/signals";
import { inject, isDevMode } from "@angular/core";
import { IApplicationUser } from "../models/applicationuser.model";
import { IAccessToken } from "../models/token.model";
import { IdentityService } from "../services/identity-service.service";
import { setError, setFulfilled, setPending, withRequestStatus } from "./request-status.store";
import { ILoginRequest } from "../dtos/login-request.dto";
import { Router } from "@angular/router";


type UserState = {
  user: IApplicationUser | null;
  access_token: IAccessToken | null;
}

const userState = signal<UserState>({
  user: null,
  access_token: null
});

export const UserStore = signalStore(
  { providedIn: 'root' },
  withState(userState),
  withRequestStatus(),
  withMethods((store, service = inject(IdentityService), router = inject(Router)) => ({
    getUserInfo() {
      patchState(store, setPending());
      service.getUserInfo().subscribe({
        next: (response) => {
          patchState(store, { user: response.body }, setFulfilled())
        },
        error: error => patchState(store, setError(error)),
        complete: () => {
          if (isDevMode())
            console.log("resquest complete");
        }
      })
    },
    login(request: ILoginRequest) {
      patchState(store, setPending);
      service.loginApplication(request).subscribe({
        next: (response) => {
          patchState(store, setPending());
          if(response.body?.tokenAccess !== undefined)
          {
            patchState(store, {access_token: response.body }, setFulfilled());
            router.navigate(['/chat'])
          }
          else
          patchState(store, {access_token: undefined }, setFulfilled());

        },
        error: error => patchState(store, setError(error))
      });
    },
    logout(){
      patchState(store, setPending());
      service.logout().subscribe({
        next: (response) => {
          if(response)
          patchState(store, {access_token: undefined}, setFulfilled())
        },
        error: (err) => patchState(store, setError(err))
      });
    }
  }))
);

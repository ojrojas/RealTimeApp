import { inject, isDevMode, signal } from "@angular/core";
import { patchState, signalStore, withMethods, withState } from "@ngrx/signals";
import { IApplicationUser } from "../models/applicationuser.model";
import { IAccessToken } from "../models/token.model";
import { IdentityService } from "../services/identity-service.service";
import { setError, setFulfilled, setPending, withRequestStatus } from "./request-status.store";
import { ILoginRequest } from "../dtos/login.request.dto";
import { Router } from "@angular/router";

type UserState = {
  user: IApplicationUser | null;
  users: IApplicationUser[] | undefined;
  access_token: IAccessToken | null;
  userSelected: IApplicationUser | undefined;
}

const userState = signal<UserState>({
  user: null,
  users: undefined,
  access_token: null,
  userSelected: undefined
});

export const UserStore = signalStore(
  { providedIn: 'root' },
  withState(userState),
  withRequestStatus(),
  withMethods((store, service = inject(IdentityService), router = inject(Router)) => ({
    getUserInfo() {
      patchState(store, setPending());
      service.getUserInfo(store.access_token()?.tokenAccess!).subscribe({
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
    getUsersConnected() {
      patchState(store, setPending());
      service.getUsersConnected(store.access_token()?.tokenAccess!).subscribe({
        next: (response) => {
          if (response.body?.length !== 0) {
            var users = response.body?.filter(x => x.id !== store.user()?.id);
            patchState(store, { users: users }, setFulfilled());
            console.log("users not equal to zero", response.body as IApplicationUser[]);
          }
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
          if (response.body?.tokenAccess !== undefined) {
            patchState(store, { access_token: response.body }, setFulfilled());
            router.navigate(['/chat'])
          }
          else
            patchState(store, { access_token: undefined }, setFulfilled());

        },
        error: error => patchState(store, setError(error))
      });
    },
    logout() {
      patchState(store, setPending());
      service.logout(store.access_token()?.tokenAccess!).subscribe({
        next: (response) => {
          if (response){
            patchState(store, { access_token: undefined }, setFulfilled());
          }
        },
        error: (err) => patchState(store, setError(err))
      });
    },
    setUserSelected(userSelected: IApplicationUser) {
      patchState(store, setPending());
      patchState(store, { userSelected: userSelected }, setFulfilled());
    }}))
);

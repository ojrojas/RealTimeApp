import { inject, Injectable } from '@angular/core';
import { IApplicationUser } from '../models/applicationuser.model';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { ILoginRequest } from '../dtos/login.request.dto';
import { environment } from '../../../environments/environment';
import { IAccessToken } from '../models/token.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  readonly http = inject(HttpClient);

  constructor() { }

  getUserInfo = (token:string): Observable<HttpResponse<IApplicationUser>> => {

    return this.http.get<IApplicationUser>(
      `${environment.api}/connect/userinfo`, {
        headers: {["Authorization"]: `Bearer ${token}` },
      observe: 'response'
    });
  }

  getUsersConnected = (token:string): Observable<HttpResponse<IApplicationUser[]>> => {

    return this.http.get<IApplicationUser[]>(
      `${environment.api}/usersconnected`, {
        headers: {["Authorization"]: `Bearer ${token}` },
      observe: 'response'
    });
  }

  logout = (token:string): Observable<HttpResponse<any>> => {
    return this.http.get(`${environment.api}/connect/logout`, { observe: 'response',
       headers: {["Authorization"]: `Bearer ${token}` },
     });
  }

  loginApplication(request: ILoginRequest): Observable<HttpResponse<IAccessToken>> {
    return this.http.post<IAccessToken>(`${environment.api}/connect/token`, request, { observe: 'response' });
  }
}

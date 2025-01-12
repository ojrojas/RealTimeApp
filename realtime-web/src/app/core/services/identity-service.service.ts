import { inject, Injectable } from '@angular/core';
import { IApplicationUser } from '../models/applicationuser.model';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { ILoginRequest } from '../dtos/login-request.dto';
import { environment } from '../../../environments/environment';
import { IAccessToken } from '../models/token.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  readonly http = inject(HttpClient);
  constructor() { }

  getUserInfo = (): Observable<HttpResponse<IApplicationUser>> => {
    return this.http.get<IApplicationUser>(
      `${environment.api}/connect/userinfo`, {
      observe: 'response'
    });
  }

  logout = (): Observable<HttpResponse<Object>> => {
    return this.http.get(`${environment.api}/connect/logout`, { observe: 'response' });
  }

  loginApplication(request: ILoginRequest): Observable<HttpResponse<IAccessToken>> {
    return this.http.post<IAccessToken>(`${environment.api}/connect/token`, request, { observe: 'response' });
  }
}
import { Injectable } from '@angular/core';
import { Login } from 'src/app/models/authentication/login';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Authenticate } from 'src/app/models/authentication/authenticate';
import { environment } from 'src/environments/environment';
import { Observable, observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(public httpClient: HttpClient) { }

  private readonly USER_STATE = '_USER_STATE';
  private readonly REMEMBER_ME = '_REMEMBER_ME';
  private loggedUser: string;

  login(password:string, user: string, rememberMe: boolean): Promise<void> {
    const credentials: Login = {
      password: password,
      user: user,
      rememberMe: rememberMe
    };
    
    return this.httpClient.post<Authenticate>(`${environment.API_BASE_URL}/Login/Authenticate`, credentials)
      .toPromise()
      .then(token => {
        if(token != null && token.id > 0)
          this.doLogin(credentials.user, {...token});
        else
          throwError(() => "fkdfasdf")
      });
  }

  private doLogin(username: string, data: Authenticate) {
    this.loggedUser = username;
    this.storeTokens(data);
  }

  private storeTokens(data: Authenticate) {
    const item = {
      id: data.id,
      fullName: data.nombres + ' ' + data.apellidos,
      email: data.correo,
    };
    //localStorage.setItem(this.REMEMBER_ME, JSON.stringify(data.rememberMe));

    // if (data.rememberMe) {
    //   localStorage.removeItem(this.USER_STATE);
    //   localStorage.setItem(this.USER_STATE, JSON.stringify(item));
    // } else {
      sessionStorage.removeItem(this.USER_STATE);
      sessionStorage.setItem(this.USER_STATE, JSON.stringify(item));
    //}
  }
}

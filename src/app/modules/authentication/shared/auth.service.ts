import { Injectable } from '@angular/core';
import { Login } from 'src/app/models/authentication/login';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Authenticate } from 'src/app/models/authentication/authenticate';
import { environment } from 'src/environments/environment';
import { Observable, observable, throwError } from 'rxjs';
import { ResetPassword } from 'src/app/models/authentication/reset-password';
import { Module } from 'src/app/models/security/module';
import { Permissions } from 'src/app/models/security/permissions';
import { PermissionxModule } from 'src/app/models/security/permissionxmodule';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(public httpClient: HttpClient) { }

  private readonly USER_STATE = '_USER_STATE';
  private readonly REMEMBER_ME = '_REMEMBER_ME';
  private readonly ACCESS_STATE = '_ACCESS_STATE';
  permissionsList: number[] = [];
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
          this.doLogin(credentials.user, {...token});
          this.getPermissionsxUser(token.id);
      });
  }

  private doLogin(username: string, data: Authenticate) {
    this.loggedUser = username;
    this.storeTokens(data);
  }

  private storeTokens(data: Authenticate) {
    const item = {
      id: data.id,
      fullName: data.persona.nombres,
      email: data.persona.correo,
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

  isLoggedIn() {
    return !!this.getUserState();
  }

  getUserState(): {} {
    let userState: {};
    const localStorageTmp = JSON.parse(localStorage.getItem(this.USER_STATE));
    const sessionStorageTmp = JSON.parse(sessionStorage.getItem(this.USER_STATE));
    if (localStorageTmp !== null) {
      userState = localStorageTmp;
    } else {
      userState = sessionStorageTmp;
    }
    return userState;
  }

  get storeUser() {
    // if (this.rememberMe === true) {
    //   return JSON.parse(localStorage.getItem(this.USER_STATE));
    // }
    return JSON.parse(sessionStorage.getItem(this.USER_STATE));
  }

  get rememberMe() {
    return localStorage.getItem(this.REMEMBER_ME) === 'true';
  }

  get idUser() {
    return this.storeUser?.id ?? '';
  }

  get entityName() {
    return this.storeUser?.fullName ?? '';
  }

  get userName() {
    return this.storeUser?.email ?? '';
  }

  doLogout() {
    this.loggedUser = null;
    this.removeTokens();
  }

  private removeTokens() {
    if (this.rememberMe === true) {
      localStorage.removeItem(this.USER_STATE);
    } else {
      sessionStorage.removeItem(this.USER_STATE);
    }
    localStorage.removeItem(this.REMEMBER_ME);
  }

  resetPassword(data: ResetPassword) {
    return this.httpClient.post<number>(`${environment.API_BASE_URL}/Login/ResetPassword`, data)
      .toPromise()
  }

  validateCode(data: ResetPassword) {
    return this.httpClient.post<number>(`${environment.API_BASE_URL}/Login/ValidateCode`, data)
      .toPromise()
  }

  updatePassword(data: ResetPassword) {
    return this.httpClient.post<boolean>(`${environment.API_BASE_URL}/Login/updatePassword`, data)
      .toPromise()
  }

  getModules(){
    return this.httpClient.get<PermissionxModule[]>(`${environment.API_BASE_URL}/Login/GetModules`)
      .toPromise();
  }

  getPermissionsxUser(idUser: number){
    this.getPermissionsPromise(idUser).then(permissions => {
      this.sendToStorage(permissions)
    })
  }

  getPermissionsPromise(idUser: number) {
    return this.httpClient.get<Permissions[]>(
      `${environment.API_BASE_URL}/Login/GetPermissionsxUser?idUser=${idUser}`)
      .toPromise()
      .then(result => result )
      .catch( error => {
        return error;
      });
  }

  sendToStorage(result: Permissions[]) {
    localStorage.setItem(this.ACCESS_STATE, JSON.stringify(result));
    
  }

  get permissions() {
    var accesses: Permissions[] = JSON.parse(localStorage.getItem(this.ACCESS_STATE));
    if (accesses == null) {
      accesses = []
    }
    this.permissionsList = [];
    Object.values(accesses).map(item => {
            this.permissionsList.push(item.id);
  });
  return this.permissionsList;
  }

  async getPermissions() {
      var accesses: Permissions[] = JSON.parse(localStorage.getItem(this.ACCESS_STATE));
      if (accesses == null) {
        accesses = []
      }
      Object.values(accesses).map(item => {
        this.permissionsList.push(item.id);
      });
      await this.permissionsList;
  }
}

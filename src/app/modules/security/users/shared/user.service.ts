import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from 'src/app/models/security/user';
import { UserFilter } from 'src/app/models/security/user-filter';
import { HttpHelpersService } from 'src/app/modules/utils/http-helpers.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient, private _httpHelpersService: HttpHelpersService) { }

  getUsers(filter: UserFilter) {
    return this.httpClient.get<User[]>(`${environment.API_BASE_URL}/User/GetUsers`, {
      params: this._httpHelpersService.getHttpParamsFromPlainObject(filter)
    })
      .toPromise();
  }

  postUser(data: User) {
    return this.httpClient.post<number>(`${environment.API_BASE_URL}/User`, data)
      .toPromise();
  }
}

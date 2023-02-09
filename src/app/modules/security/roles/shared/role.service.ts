import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Permissions } from 'src/app/models/security/permissions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private httpClient: HttpClient) { }

  getPermissions() {
    return this.httpClient.get<Permissions[]>(`${environment.API_BASE_URL}/Role/GetPermissions`)
      .toPromise();
  }
}

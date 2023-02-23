import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Permissions } from 'src/app/models/security/permissions';
import { PermissionxModule } from 'src/app/models/security/permissionxmodule';
import { Role } from 'src/app/models/security/role';
import { RoleFilter } from 'src/app/models/security/role-filters';
import { HttpHelpersService } from 'src/app/modules/utils/http-helpers.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private httpClient: HttpClient, private _httpHelpersService: HttpHelpersService) { }

  getPermissionsxModule() {
    return this.httpClient.get<PermissionxModule[]>(`${environment.API_BASE_URL}/Role/GetPermissionsxModule`)
      .toPromise();
  }

  getRoles(filter: RoleFilter) {
    return this.httpClient.get<Role[]>(`${environment.API_BASE_URL}/Role/GetRoles`, {
      params: this._httpHelpersService.getHttpParamsFromPlainObject(filter)
    })
      .toPromise();
  }

  postRole(data: Role) {
    return this.httpClient.post<number>(`${environment.API_BASE_URL}/Role`, data)
      .toPromise();
  }
}

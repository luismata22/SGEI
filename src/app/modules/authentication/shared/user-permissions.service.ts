import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserPermissionsService {

  permissions: number[] = [];
  constructor(private authService: AuthService) {
    Object.values({ ...this.authService.permissions }).map((item: any) => {
      this.permissions.push(item);
    });

    //debugger;
    //this.getPermissions();
  }

  ngOnInit() { }

  allowed = (permissionId: number) => this.permissions.includes(permissionId);

  async getPermissions() {
    await this.authService.permissions.forEach(item => {
      this.permissions.push(item);
    });
  }
}

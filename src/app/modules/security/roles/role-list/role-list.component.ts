import { Component, OnInit } from '@angular/core';
import { Permissions } from 'src/app/models/security/permissions';
import { NotificationService } from 'src/app/modules/utils/notification.service';
import { RoleService } from '../shared/role.service';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss']
})
export class RoleListComponent implements OnInit {

  permissionsList: Permissions[] = [];

  constructor(private roleService: RoleService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.getPermissions();
  }

  getPermissions() {
    this.roleService.getPermissions()
      .then(data => {
        this.permissionsList = [...data];
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error", "Error")
      });
  }

  saveRole(){
    
  }
}

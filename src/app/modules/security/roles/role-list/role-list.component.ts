import { Component, OnInit } from '@angular/core';
import { Permissions } from 'src/app/models/security/permissions';
import { PermissionxRole } from 'src/app/models/security/permissionxrole';
import { Role } from 'src/app/models/security/role';
import { RoleFilter } from 'src/app/models/security/role-filters';
import { NotificationService } from 'src/app/modules/utils/notification.service';
import { RoleService } from '../shared/role.service';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss']
})
export class RoleListComponent implements OnInit {

  tittleModal = "Nuevo rol";
  permissionsList: Permissions[] = [];
  roleList: Role[] = [];
  role: Role = new Role();
  save: boolean = false;
  filter: RoleFilter = new RoleFilter();

  constructor(private roleService: RoleService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.getRoles();
    this.getPermissions();
  }

  getRoles() {
    this.roleService.getRoles(this.filter)
      .then(data => {
        console.log(data)
        this.roleList = [...data];
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error", "Error")
      });
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

  saveRole(event) {
    this.save = true;
    if(this.role.nombre != ""){
      if(this.permissionsList.filter(x => x.checked).length > 0){
        this.role.permisosxrole = [];
        this.permissionsList.filter(x => x.checked).forEach(permission => {
          this.role.permisosxrole.push({
            id: -1,
            idpermiso: permission.id,
            idrol: -1,
            activo: true,
            permiso: new Permissions()
          });
        });
        if(this.role.id <= 0){
          this.role.activo = true;
        }else{
          this.role.activo = Boolean(JSON.parse(this.role.activo.toString()))
        }
        this.roleService.postRole(this.role)
        .then(data => {
          this.save = false;
          if(data > 0){
            this.getRoles();
            this.notificationService.showSuccess("Rol guardado exitosamente", "Éxito")
            event.hide();
          }else if(data == -1){
            this.notificationService.showWarning("Rol ya registrado", "Alerta")
          }else{
            this.notificationService.showError("Ha ocurrido un error", "Error")
          }
        })
        .catch(error => {
          console.log(error)
          this.notificationService.showError("Ha ocurrido un error", "Error")
        });
      }else{
        this.notificationService.showWarning("Seleccione un permiso", "Alerta")
      }
    }
  }

  showModal(event){
    this.tittleModal = "Nuevo rol";
    event.show();
    this.role = new Role();
    this.permissionsList.map(x => {
      x.checked = false;
    })
  }

  editModal(event, role: Role){
    this.tittleModal = "Editar rol";
    event.show();
    this.role = {...role};
    this.permissionsList.map(x => {
      x.checked = false;
    })
    role.permisosxrole.map(x => {
      this.permissionsList.find(p => p.id == x.idpermiso).checked = true;
    })
  }
}

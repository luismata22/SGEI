import { Component, OnInit } from '@angular/core';
import { Module } from 'src/app/models/security/module';
import { Permissions } from 'src/app/models/security/permissions';
import { PermissionxModule } from 'src/app/models/security/permissionxmodule';
import { PermissionxRole } from 'src/app/models/security/permissionxrole';
import { Role } from 'src/app/models/security/role';
import { RoleFilter } from 'src/app/models/security/role-filters';
import { dtOptions } from 'src/app/modules/utils/dataTableOptions';
import { NotificationService } from 'src/app/modules/utils/notification.service';
import { RoleService } from '../shared/role.service';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss']
})
export class RoleListComponent implements OnInit {

  tittleModal = "Nuevo rol";
  permissionsxmoduleList: PermissionxModule[] = [];
  modulesList: Module[] = [];
  roleList: Role[] = [];
  role: Role = new Role();
  save: boolean = false;
  filter: RoleFilter = new RoleFilter();
  dtOptions = dtOptions;

  constructor(private roleService: RoleService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.getRoles();
    this.getPermissionsxModule();
  }

  getRoles() {
    this.roleService.getRoles(this.filter)
      .then(data => {
        this.roleList = [];
        this.roleList = [...data];
        console.log(this.roleList)
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error", "Error")
      });
  }

  gerPermissionxmodule(module: Module){
    return this.permissionsxmoduleList.filter(x => x.idmodulo == module.id);
  }

  getPermissionsxModule() {
    this.roleService.getPermissionsxModule()
      .then(data => {
        if(data.length > 0){
          data.forEach(pxm => {
            if(this.modulesList.filter(x => x.id == pxm.idmodulo).length == 0){
              this.modulesList.push(pxm.modulo)
            }
          })
        }
        this.permissionsxmoduleList = [...data];
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error al obtener los permisos por modulo", "Error")
      });
  }

  saveRole(event) {
    this.save = true;
    if(this.role.nombre != ""){
      if(this.permissionsxmoduleList.filter(x => x.checked).length > 0){
        this.role.permisosxmoduloxrole = [];
        this.permissionsxmoduleList.filter(x => x.checked).forEach(permissionxmodule => {
          this.role.permisosxmoduloxrole.push({
            id: -1,
            idpermisoxmodulo: permissionxmodule.id,
            idrol: -1,
            activo: true,
            rol: new Role(),
            permisosxmodulo: new PermissionxModule()
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
    this.permissionsxmoduleList.map(x => {
      x.checked = false;
    })
  }

  editModal(event, role: Role){
    this.tittleModal = "Editar rol";
    event.show();
    this.role = {...role};
    this.permissionsxmoduleList.map(x => {
      x.checked = false;
    })
    console.log(role);
    role.permisosxmoduloxrole.map(x => {
      this.permissionsxmoduleList.find(p => p.id == x.idpermisoxmodulo).checked = true;
    })
  }
}

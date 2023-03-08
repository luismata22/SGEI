import { Component, OnInit, ViewChild } from '@angular/core';
import { ActiveReportsModule, AR_EXPORTS, HtmlExportService, PdfExportService, ViewerComponent, XlsxExportService } from '@grapecity/activereports-angular';
import { Person } from 'src/app/models/master/person';
import { Role } from 'src/app/models/security/role';
import { RoleFilter } from 'src/app/models/security/role-filters';
import { User } from 'src/app/models/security/user';
import { UserFilter } from 'src/app/models/security/user-filter';
import { EncryptService } from 'src/app/modules/authentication/shared/encrypt.service';
import { StudentsService } from 'src/app/modules/master/students/shared/students.service';
import { dtOptions } from 'src/app/modules/utils/dataTableOptions';
import { NotificationService } from 'src/app/modules/utils/notification.service';
import { RoleService } from '../../roles/shared/role.service';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})
export class UsersListComponent implements OnInit {

  tittleModal = "Nuevo usuario";
  save: boolean = false;
  repetirClave: string = "";
  user: User = new User();
  filter: UserFilter = new UserFilter();
  userList: User[] = [];
  roleList: any[] = [];
  selectedRoles: any[] = [];
  dropdownSettings = {};
  regexEmail = new RegExp("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
  validateEmail: boolean = true;
  searchPerson: boolean = false;
  maxLengthPassword = 8;
  dtOptions = dtOptions;

  constructor(private userService: UserService,
    private notificationService: NotificationService,
    private encryptService: EncryptService,
    private roleService: RoleService,
    private studentsService: StudentsService) {

  }

  ngOnInit(): void {
    this.dropdownSettings = {
      singleSelection: false,
      text: "Seleccionar roles",
      idField: "id",
      textField: "itemName",
      selectAllText: 'Seleccionar todo',
      unSelectAllText: 'Deseleccionar todo',
      enableSearchFilter: true,
      enableFilterSelectAll: false,
      searchPlaceholderText: "Buscar",
      noDataLabel: "No hay datos para mostrar",
      classes: "myclass custom-class"
    };
    this.getRoles();
    this.getUsers();
  }

  getRoles() {
    this.roleService.getRoles({ ...new RoleFilter() })
      .then(data => {
        var list: any[] = [];
        data.map(x => {
          list.push({
            "id": x.id,
            "itemName": x.nombre
          })
        })
        this.roleList = [...list]
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error", "Error")
      });
  }

  onItemSelect(value) {
    console.log(value)
  }

  getUsers() {
    this.userService.getUsers(this.filter)
      .then(data => {
        console.log(data)
        this.userList = [...data];
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error", "Error")
      });
  }

  showModal(event) {
    this.tittleModal = "Nuevo usuario";
    this.user = new User();
    this.selectedRoles = [];
    this.repetirClave = "";
    event.show();
  }

  saveUser(event) {
    this.save = true;
    if (this.user.persona.nombres != "" && this.user.persona.apellidos != "" && this.user.persona.cedula != "" && this.user.persona.correo != "" && this.user.clave != "" && this.repetirClave != ""
      && this.user.clave.length >= this.maxLengthPassword && this.user.clave == this.repetirClave) {
      if (this.regexEmail.test(this.user.persona.correo)) {
        this.validateEmail = true;
        if (this.selectedRoles.length > 0) {
          this.user.rolesxusuario = [];
          this.selectedRoles.forEach(rol => {
            this.user.rolesxusuario.push({
              id: -1,
              idusuario: -1,
              idrol: rol.id,
              activo: true,
              rol: new Role()
            });
          });
          const passwordEncrypt: string = this.encryptService.encrypt(this.user.clave, this.user.persona.correo);
          //this.user.clave = passwordEncrypt;
          if (this.user.id <= 0) {
            this.user.activo = true;
          } else {
            this.user.activo = Boolean(JSON.parse(this.user.activo.toString()))
          }
          this.userService.postUser({ ...this.user, clave: passwordEncrypt })
            .then(data => {
              this.save = false;
              if (data > 0) {
                this.getUsers();
                this.notificationService.showSuccess("Usuario guardado exitosamente", "Éxito")
                event.hide();
              } else if (data == -1) {
                this.notificationService.showWarning("Usuario ya registrado", "Alerta")
              } else {
                this.notificationService.showError("Ha ocurrido un error", "Error")
              }
            })
            .catch(error => {
              console.log(error)
              this.notificationService.showError("Ha ocurrido un error", "Error")
            });
        } else {
          this.notificationService.showWarning("Seleccione un rol", "Alerta")
        }
      } else {
        this.validateEmail = false;
      }

    }
  }

  editModal(event, user: User) {
    this.tittleModal = "Editar ususario";
    this.user = user;
    var list: any[] = []
    user.rolesxusuario.map(rolexuser => {
      list.push({
        "id": rolexuser.rol.id,
        "itemName": rolexuser.rol.nombre
      })
    })
    this.selectedRoles = [...list];
    this.user.clave = "";
    this.repetirClave = "";
    event.show();
  }

  searchPersonbyDocumentNumber() {
    this.searchPerson = true;
    if (this.user.persona != undefined && this.user.persona.cedula != "") {
      this.studentsService.getPersonByDocumentNumber(this.user.persona.cedula)
        .then(data => {
          this.searchPerson = false;
          if (data != null && data.id > 0) {
            this.userService.getUserByIdPerson(data.id)
              .then(dataUser => {
                if (dataUser != null && dataUser.id > 0) {
                  this.notificationService.showError("Ésta cédula ya se encuentra registrada", "Error")
                } else {
                  this.user.persona = { ...data };
                }
              })
              .catch(error => {
                console.log(error)
                this.notificationService.showError("Ha ocurrido un error al buscar la cédula", "Error")
              });
          } else {
            this.notificationService.showError("Ésta cédula no se encuentra registrada", "Error")
          }
        })
        .catch(error => {
          console.log(error)
          this.notificationService.showError("Ha ocurrido un error al buscar la cédula", "Error")
        });
    }
  }

  clearPerson() {
    this.user.persona.id = -1;
    this.user.persona.correo = "";
    this.user.persona.apellidos = "";
    this.user.persona.nombres = "";
    this.user.persona.direccion = "";
    this.user.persona.fechanacimiento = new Date();
    this.user.persona.fechanacimientoString = "";
    this.user.persona.telefono = "";
  }
}

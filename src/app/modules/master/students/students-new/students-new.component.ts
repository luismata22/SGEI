import { Component, EventEmitter, OnInit } from '@angular/core';
import { Student } from 'src/app/models/master/student';
import { Person } from 'src/app/models/master/person'
import { TypeCourse } from 'src/app/models/master/typecourse';
import { NotificationService } from 'src/app/modules/utils/notification.service';
import { StudentsService } from '../shared/students.service';
import { dtOptions } from 'src/app/modules/utils/dataTableOptions';
import { ActivatedRoute, Router } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { PersonxStudent } from 'src/app/models/master/personsxstudent';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-students-new',
  templateUrl: './students-new.component.html',
  styleUrls: ['./students-new.component.scss'],
  providers: [DatePipe]
})
export class StudentsNewComponent implements OnInit {

  student: Student = new Student();
  typeCourseList: TypeCourse[] = [];
  tittleModal: string = "Agregar representante";
  tittleString: string = "Crear estudiante"
  person: Person = new Person();
  savePer: boolean = false;
  saveStu: boolean = false;
  dtOptions = dtOptions;
  personsList: Person[] = [];
  regexEmail = new RegExp("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
  validateEmail: boolean = true;

  public uploader: FileUploader = new FileUploader({
    url: 'http://localhost:3000/fileupload/',
    disableMultipart: false,
    autoUpload: true,
    method: 'post',
    itemAlias: 'attachment',
    allowedFileType: ['image', 'pdf']
  });

  constructor(private studentsService: StudentsService,
    private notificationService: NotificationService,
    private router: Router,
    private actRoute: ActivatedRoute,
    private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.getTypeCourses();
    var idStudent = parseInt(this.actRoute.snapshot.params['id']);
    if (idStudent > 0) {
      this.tittleString = "Editar estudiante";
      this.getStudent(idStudent);
    } else {
      this.tittleString = "Crear estudiante";
    }
  }

  getStudent(idStudent: number) {
    this.studentsService.getStudentById(idStudent)
      .then(data => {
        this.student = { ...data };
        this.student.fechanacimientoString = this.datePipe.transform(new Date(data.fechanacimiento), "yyyy-MM-dd");
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error al cargar el estudiante", "Error")
      });
  }

  getTypeCourses() {
    this.studentsService.getTypeCourses()
      .then(data => {
        console.log(data);
        this.typeCourseList = [...data];
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error", "Error")
      });
  }

  savePerson(modal) {
    this.savePer = true;
    if (this.person.nombres != "" && this.person.apellidos != "" && this.person.cedula != "" && this.person.correo != ""
      && this.person.direccion != "" && this.person.fechanacimientoString != "") {
      if (this.regexEmail.test(this.person.correo)) {
        this.validateEmail = true;
        this.savePer = false;
        var strdate = this.person.fechanacimientoString.split('-');
        this.person.fechanacimiento = new Date(parseInt(strdate[0]), parseInt(strdate[1]) - 1, parseInt(strdate[2]));
        this.student.personasxestudiante.push({
          id: -1,
          idestudiante: -1,
          idpersona: -1,
          activo: false,
          persona: this.person,
          estudiante: new Student(),
          esrepresentante: false
        });
        modal.hide();
      } else {
        this.validateEmail = false;
      }
    }
  }

  addPerson(modal) {
    this.person = new Person();
    this.tittleModal = "Nueva persona";
    modal.show();
  }

  editModal(modal, representative: Person) {
    this.tittleModal = "Editar persona";
    this.person = { ...representative };
    this.person.fechanacimientoString = this.datePipe.transform(new Date(this.person.fechanacimiento), "yyyy-MM-dd");
    modal.show();
  }

  clearPerson() {
    this.student = new Student();
  }

  back() {
    this.router.navigate(["/students"])
  }

  clear() {
    this.student = new Student();
  }

  saveStudent() {
    this.saveStu = true;
    if (this.student.nombres != "" && this.student.apellidos != "" && this.student.idtipocurso > 0 && this.student.fechanacimientoString != "") {
      if (this.student.personasxestudiante.length > 0) {
        if (this.student.personasxestudiante.filter(x => x.esrepresentante).length > 0) {
          var strdate = this.student.fechanacimientoString.split('-');
          this.student.fechanacimiento = new Date(parseInt(strdate[0]), parseInt(strdate[1]) - 1, parseInt(strdate[2]));
          //this.student.fechanacimiento = new Date(this.student.fechanacimientoString);
          this.studentsService.postStudent({ ...this.student })
            .then(data => {
              this.saveStu = false;
              if (data > 0) {
                this.notificationService.showSuccess("Estudiante guardado exitosamente", "Éxito")
              } else if (data == -1) {
                this.notificationService.showWarning("Estudiante ya registrado", "Alerta")
              } else {
                this.notificationService.showError("Ha ocurrido un error", "Error")
              }
            })
            .catch(error => {
              console.log(error)
              this.notificationService.showError("Ha ocurrido un error", "Error")
            });
        } else {
          this.notificationService.showWarning("Seleccione una persona como representante", "Alerta")
        }
      } else {
        this.notificationService.showWarning("Asigne un representante", "Alerta")
      }
    }
  }

  getPersons() {
    this.studentsService.getPersons()
      .then(data => {
        this.personsList = [...data];
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error al obtener las personas", "Error")
      });
  }

  searchPerson(modal) {
    this.getPersons();
    modal.show();
  }

  selectPerson(representante: Person, modal) {
    if (this.student.personasxestudiante.filter(x => x.persona.id == representante.id).length == 0) {
      this.student.personasxestudiante.push({
        id: -1,
        idestudiante: -1,
        idpersona: -1,
        activo: false,
        persona: representante,
        estudiante: new Student(),
        esrepresentante: false
      });
      modal.hide();
    } else {
      this.notificationService.showWarning("Esta representante ya se encuentra en la lista", "Alerta")
    }

  }

  public onFileSelected(event: EventEmitter<File[]>) {
    const file: File = event[0];
    console.log(file);
  }

  checkSelected(personxStudent: PersonxStudent) {
    this.student.personasxestudiante.filter(x => x != personxStudent).map(x => {
      x.esrepresentante = false;
    })
  }
}

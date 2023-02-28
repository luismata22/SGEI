import { Component, OnInit } from '@angular/core';
import { Student } from 'src/app/models/master/student';
import { Person } from 'src/app/models/master/person'
import { TypeCourse } from 'src/app/models/master/typecourse';
import { NotificationService } from 'src/app/modules/utils/notification.service';
import { StudentsService } from '../shared/students.service';
import { dtOptions } from 'src/app/modules/utils/dataTableOptions';
import { Router } from '@angular/router';

@Component({
  selector: 'app-students-new',
  templateUrl: './students-new.component.html',
  styleUrls: ['./students-new.component.scss']
})
export class StudentsNewComponent implements OnInit {

  student: Student = new Student();
  typeCourseList: TypeCourse[] = [];
  tittleModal: string = "Agregar representante";
  person: Person = new Person();
  savePer: boolean = false;
  saveStu: boolean = false;
  dtOptions = dtOptions;

  constructor(private studentsService: StudentsService,
    private notificationService: NotificationService,
    private router: Router) { }

  ngOnInit(): void {
    this.getTypeCourses();
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
      && this.person.direccion != "" && this.person.fechanacimiento != null) {
      if (this.student.representantes.filter(x => x.esrepresentante).length == 0) {
        this.savePer = false;
        this.student.representantes.push(this.person);
        modal.hide();
      } else {
        this.notificationService.showError("Ya existe una persona como representante", "Error")
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
    if (this.student.nombres != "" && this.student.apellidos != "" && this.student.idtipocurso > 0 && this.student.fechanacimiento != null) {
      if(this.student.representantes.length > 0){
        console.log(this.student)
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
      }else{
        this.notificationService.showWarning("Asigne un representante", "Alerta")
      }
    }
  }
}

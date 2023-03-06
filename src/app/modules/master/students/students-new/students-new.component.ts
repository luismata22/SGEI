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

@Component({
  selector: 'app-students-new',
  templateUrl: './students-new.component.html',
  styleUrls: ['./students-new.component.scss']
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

  constructor(private studentsService: StudentsService,
    private notificationService: NotificationService,
    private router: Router,
    private actRoute: ActivatedRoute,) { }

  ngOnInit(): void {
    this.getTypeCourses();
    var idStudent = parseInt(this.actRoute.snapshot.params['id']);
    if(idStudent > 0){
      this.tittleString = "Editar estudiante";
      this.getStudent(idStudent);
    }else{
      this.tittleString = "Crear estudiante";
    }
  }

  public uploader: FileUploader = new FileUploader({
    url: "http://localhost:3000/fileupload/",
    disableMultipart : false,
    autoUpload: true,
    method: 'post',
    itemAlias: 'attachment',
    allowedFileType: ['image', 'pdf']
  });

  getStudent(idStudent: number){

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
      if (this.student.personasxestudiante.filter(x => x.esrepresentante).length == 0) {
        this.savePer = false;
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
      if (this.student.personasxestudiante.length > 0) {
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
    if(this.student.personasxestudiante.filter(x => x.persona.id == representante.id).length == 0){
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
    }else{
      this.notificationService.showWarning("Esta representante ya se encuentra en la lista", "Alerta")
    }
    
  }

  public onFileSelected(event: EventEmitter<File[]>) {
    const file: File = event[0];
    console.log(file);
  }

  checkSelected(personxStudent: PersonxStudent){
    this.student.personasxestudiante.filter(x => x != personxStudent).map(x => {
      x.esrepresentante = false;
    })
  }
}

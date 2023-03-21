import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Student } from 'src/app/models/master/student';
import { NotificationService } from 'src/app/modules/utils/notification.service';
import { StudentsService } from '../shared/students.service';

@Component({
  selector: 'app-student-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.scss'],
  providers: [DatePipe]
})
export class StudentProfileComponent implements OnInit {

  student: Student = new Student();
  imageProfile: SafeUrl;

  constructor(private actRoute: ActivatedRoute,
    private studentsService: StudentsService,
    private datePipe: DatePipe,
    private notificationService: NotificationService,
    private _sanitizationService: DomSanitizer) { }

  ngOnInit(): void {
    var idStudent = parseInt(this.actRoute.snapshot.params['id']);
    this.getStudent(idStudent);
  }

  getStudent(idStudent: number) {
    this.studentsService.getStudentById(idStudent)
      .then(data => {
        this.student = { ...data };
        console.log(this.student);
        this.imageProfile = this._sanitizationService.bypassSecurityTrustUrl(this.student.archivosxestudiante.filter(x => x.indperfil).length > 0 ? this.student.archivosxestudiante.find(x => x.indperfil).nombre : "");
        this.student.fechanacimientoString = this.datePipe.transform(new Date(data.fechanacimiento), "yyyy-MM-dd");
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error al cargar el estudiante", "Error")
      });
  }
}

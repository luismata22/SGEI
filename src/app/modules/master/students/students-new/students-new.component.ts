import { Component, OnInit } from '@angular/core';
import { Student } from 'src/app/models/master/student';
import { TypeCourse } from 'src/app/models/master/typecourse';
import { NotificationService } from 'src/app/modules/utils/notification.service';
import { StudentsService } from '../shared/students.service';

@Component({
  selector: 'app-students-new',
  templateUrl: './students-new.component.html',
  styleUrls: ['./students-new.component.scss']
})
export class StudentsNewComponent implements OnInit {

  student: Student = new Student();
  typeCourseList: TypeCourse[] = [];

  constructor(private studentsService: StudentsService,
    private notificationService: NotificationService) { }

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

}

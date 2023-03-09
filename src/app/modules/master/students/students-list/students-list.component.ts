import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Student } from 'src/app/models/master/student';
import { StudentFilters } from 'src/app/models/master/student-filters';
import { dtOptions } from 'src/app/modules/utils/dataTableOptions';
import { NotificationService } from 'src/app/modules/utils/notification.service';
import { StudentsService } from '../shared/students.service';

@Component({
  selector: 'app-students-list',
  templateUrl: './students-list.component.html',
  styleUrls: ['./students-list.component.scss']
})
export class StudentsListComponent implements OnInit {

  filter: StudentFilters = new StudentFilters();
  studentList: Student[] = [];
  dtOptions = dtOptions;
  
  constructor(private router: Router,
    private studentsService: StudentsService,
    private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.getStudents();
  }

  getStudents(){
    this.studentsService.getStudents(this.filter)
      .then(data => {
        this.studentList = [];
        this.studentList = [...data];
      })
      .catch(error => {
        console.log(error)
        this.notificationService.showError("Ha ocurrido un error", "Error")
      });
  }

  newStudent(){
    this.router.navigate(["/students-new", -1])
  }

  editStudent(student: Student){
    this.router.navigate(["/students-new", student.id])
  }

  viewProfile(student: Student){
    this.router.navigate(["/student-profile", student.id])
  }
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { dtOptions } from 'src/app/modules/utils/dataTableOptions';

@Component({
  selector: 'app-students-list',
  templateUrl: './students-list.component.html',
  styleUrls: ['./students-list.component.scss']
})
export class StudentsListComponent implements OnInit {

  dtOptions = dtOptions;
  
  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  newStudent(){
    this.router.navigate(["/students-new"])
  }
}

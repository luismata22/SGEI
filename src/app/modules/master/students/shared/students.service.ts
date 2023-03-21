import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FilesxStudents } from 'src/app/models/master/filesxstudents';
import { Person } from 'src/app/models/master/person';
import { Student } from 'src/app/models/master/student';
import { StudentFilters } from 'src/app/models/master/student-filters';
import { TypeCourse } from 'src/app/models/master/typecourse';
import { HttpHelpersService } from 'src/app/modules/utils/http-helpers.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  constructor(private httpClient: HttpClient, private _httpHelpersService: HttpHelpersService) { }

  getTypeCourses() {
    return this.httpClient.get<TypeCourse[]>(`${environment.API_BASE_URL}/Student/GetTypeCourse`)
      .toPromise();
  }

  getStudents(filter: StudentFilters) {
    return this.httpClient.get<Student[]>(`${environment.API_BASE_URL}/Student`, {
      params: this._httpHelpersService.getHttpParamsFromPlainObject(filter)
    })
      .toPromise();
  }

  getStudentById(idStudent: number) {
    return this.httpClient.get<Student>(`${environment.API_BASE_URL}/Student/GetStudentById?idStudent=${idStudent}`)
      .toPromise();
  }

  getPersonByDocumentNumber(documentNumber: string) {
    return this.httpClient.get<Person>(`${environment.API_BASE_URL}/Person/GetPersonByDocumentNumber?documentNumber=${documentNumber}`)
      .toPromise();
  }

  postStudent(data: Student) {
    return this.httpClient.post<number>(`${environment.API_BASE_URL}/Student`, data)
      .toPromise();
  }

  getPersons() {
    return this.httpClient.get<Person[]>(`${environment.API_BASE_URL}/Person/GetPersons`)
      .toPromise();
  }

  deleteFile(file: FilesxStudents){
    return this.httpClient.post<number>(`${environment.API_BASE_URL}/Student/DeleteFile`, file)
      .toPromise();
  }
}

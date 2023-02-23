import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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
}

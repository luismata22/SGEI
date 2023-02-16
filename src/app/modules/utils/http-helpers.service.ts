import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HttpHelpersService {

  constructor() { }

  getHttpParamsFromPlainObject(object: any){
    var params = new HttpParams()

    for (const key in object){
      params = params.set(key,object[key]);
    }

    return params;
  }
}

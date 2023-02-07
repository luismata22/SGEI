import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr'

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastr: ToastrService) { }

  showSuccess(message, tittle){
    this.toastr.success(message, tittle);
  }

  showError(message, tittle){
    this.toastr.error(message, tittle);
  }

  showWarning(message, tittle){
    this.toastr.warning(message, tittle);
  }

  showInfo(message, tittle){
    this.toastr.info(message, tittle);
  }
}

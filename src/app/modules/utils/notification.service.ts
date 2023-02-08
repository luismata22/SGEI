import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr'

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastr: ToastrService) { }

  showSuccess(message, tittle){
    this.toastr.success(message, tittle, {
      timeOut: 3000,
    });
  }

  showError(message, tittle){
    this.toastr.error(message, tittle, {
      timeOut: 3000,
    });
  }

  showWarning(message, tittle){
    this.toastr.warning(message, tittle, {
      timeOut: 3000,
    });
  }

  showInfo(message, tittle){
    this.toastr.info(message, tittle, {
      timeOut: 3000,
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ResetPassword } from 'src/app/models/authentication/reset-password';
import { NotificationService } from '../../utils/notification.service';
import { AuthService } from '../shared/auth.service';
import { EncryptService } from '../shared/encrypt.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  resetPasswordForm: FormGroup;
  resetPasswordInvalid: boolean;
  resulvalid: boolean = false;
  resulvalidcode: boolean = false;

  constructor(private router: Router, private readonly _formBuilder: FormBuilder,
    private authService: AuthService, private notificationService: NotificationService,
    private encryptService: EncryptService) { }

  ngOnInit(): void {
    this.resetPasswordForm = this._formBuilder.group({
      user: ['', Validators.required],
      code: [''],
      password: [''],
      repeatPassword: ['']
    });

  }

  onChange(): void {
    this.resetPasswordForm.valueChanges.subscribe(() => {
      if (this.resetPasswordInvalid) {
        this.resetPasswordInvalid = false;
      }
    });
  }

  onSubmit() {
    this.resetPasswordInvalid = false;
    if (!this.resulvalid && !this.resulvalidcode) {
      if (this.resetPasswordForm.valid) {
        try {
          const credentials = new ResetPassword();
          credentials.user = this.resetPasswordForm.controls.user.value;
          this.authService.resetPassword(credentials)
            .then(data => {
              if (data)
                this.resulvalid = data;
              else
                this.notificationService.showError("Ha ocurrido un error", "Error")
            })
            .catch(error => {
              console.log(error)
              this.notificationService.showError("Ha ocurrido un error", "Error")
            });
        } catch (err) {
          this.resetPasswordInvalid = true;
        }
      }
    } else if (this.resulvalid && !this.resulvalidcode) {
      try {
        const credentials = new ResetPassword();
        credentials.user = this.resetPasswordForm.controls.user.value;
        credentials.code = this.resetPasswordForm.controls.code.value;
        this.authService.validateCode(credentials)
          .then(data => {
            if (data){
              this.resulvalid = false;
              this.resulvalidcode = data;
            }
            else
              this.notificationService.showError("Ha ocurrido un error", "Error")
          })
          .catch(error => {
            console.log(error)
            this.notificationService.showError("Ha ocurrido un error", "Error")
          });
      } catch (err) {
        this.resetPasswordInvalid = true;
      }
    } else {
      if(this.resetPasswordForm.controls.password.value == this.resetPasswordForm.controls.repeatPassword.value){
        try {
          const credentials = new ResetPassword();
          credentials.user = this.resetPasswordForm.controls.user.value;
          credentials.password = this.encryptService.encrypt(this.resetPasswordForm.controls.password.value, this.resetPasswordForm.controls.user.value);
          this.authService.updatePassword(credentials)
            .then(data => {
              if (data){
                this.notificationService.showSuccess("Contraseña cambiada exitosamente", "Éxito")
                this.router.navigate(['login'])
              }
              else
                this.notificationService.showError("Ha ocurrido un error", "Error")
            })
            .catch(error => {
              console.log(error)
              this.notificationService.showError("Ha ocurrido un error", "Error")
            });
        } catch (err) {
          this.resetPasswordInvalid = true;
        }
      }else{
        this.notificationService.showError("Las contraseñas no son iguales", "Error")
      }
    }

  }

  signin() {
    this.router.navigate(['/login']);
  }
}

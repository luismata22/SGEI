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
      user: [''],
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
      this.setCodeValidators();
      if (this.resetPasswordForm.valid) {
        try {
          const credentials = new ResetPassword();
          credentials.user = this.resetPasswordForm.controls.user.value;
          this.authService.resetPassword(credentials)
            .then(data => {
              if (data > 0) {
                this.resulvalid = true;
                this.setCodeValidators()
              }
              else if (data == -1)
                this.notificationService.showError("Correo no registrado", "Error")
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
            if (data > 0) {
              this.resulvalid = false;
              this.resulvalidcode = true;
              this.setCodeValidators()
            }
            else if (data == -1)
                this.notificationService.showError("Código no válido", "Error")
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
      if (this.resetPasswordForm.controls.password.value == this.resetPasswordForm.controls.repeatPassword.value) {
        try {
          const credentials = new ResetPassword();
          credentials.user = this.resetPasswordForm.controls.user.value;
          credentials.password = this.encryptService.encrypt(this.resetPasswordForm.controls.password.value, this.resetPasswordForm.controls.user.value);
          this.authService.updatePassword(credentials)
            .then(data => {
              if (data) {
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
      } else {
        this.notificationService.showError("Las contraseñas no son iguales", "Error")
      }
    }

  }

  setCodeValidators() {
    if (!this.resulvalid && !this.resulvalidcode) {
      this.resetPasswordForm.get('user').setValidators([Validators.required]);
      this.resetPasswordForm.get('user').updateValueAndValidity()
    } else if (this.resulvalid && !this.resulvalidcode) {
      this.resetPasswordForm.get('code').setValidators([Validators.required]);
      this.resetPasswordForm.get('code').updateValueAndValidity()
    } else {
      this.resetPasswordForm.get('code').setValidators(null);
      this.resetPasswordForm.get('password').setValidators([Validators.required]);
      this.resetPasswordForm.get('repeatPassword').setValidators([Validators.required]);
      this.resetPasswordForm.get('code').updateValueAndValidity()
      this.resetPasswordForm.get('password').updateValueAndValidity()
      this.resetPasswordForm.get('repeatPassword').updateValueAndValidity()
    }
  }

  signin() {
    this.router.navigate(['/login']);
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/authentication/login';
import { AuthService } from '../shared/auth.service';
import { EncryptService } from '../shared/encrypt.service';

@Component({
  selector: 'app-auth-signin',
  templateUrl: './auth-signin.component.html',
  styleUrls: ['./auth-signin.component.scss']
})
export class AuthSigninComponent implements OnInit {

  constructor(private router: Router,
    private readonly _formBuilder: FormBuilder,
     private encryptService: EncryptService,
    private authService: AuthService) { }

  loginForm: FormGroup;
  loginInvalid: boolean;
  rememberMe: boolean;
  private formSubmitAttempt: boolean;

  ngOnInit(): void {
    this.loginForm = this._formBuilder.group({
      user: ['', Validators.required],
      password: ['', Validators.required]
    });
    this.rememberMe = false;
    this.onChange();
  }

  resetPassword(){
    this.router.navigate(['/reset-password']);
  }

  async onSubmit() {
    this.loginInvalid = false;
    this.formSubmitAttempt = false;
    if (this.loginForm.valid) {
      try {
        const credentials = new Login();
        credentials.password = this.loginForm.controls.password.value;
        credentials.user = this.loginForm.controls.user.value;
        //credentials.rememberMe = this.rememberMe;
        this.doLogin(credentials);
      } catch (err) {
        this.loginInvalid = true;
      }
    } else {
      this.formSubmitAttempt = true;
    }
  }

  private doLogin(credentialsVM: Login) {
    const passwordEncrypt: string = this.encryptService.encrypt(credentialsVM.password, credentialsVM.user);
    this.authService.login(passwordEncrypt, credentialsVM.user, credentialsVM.rememberMe)
      .then(_ => {
          this.router.navigate(['dashboard']);
          this.loginInvalid = false;
      })
      .catch(error => {
        this.loginInvalid = true;
        //this._messageService.add({severity: 'error', summary: 'Autenticación', detail: error?.error?.message});
      });
  }

  onChange(): void {
    this.loginForm.valueChanges.subscribe( () => {
      if (this.loginInvalid) {
        this.loginInvalid = false;
      }
    });
  }
}

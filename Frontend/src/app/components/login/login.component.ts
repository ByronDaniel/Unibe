import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Login } from 'src/app/models/Login';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  formGroupLogin!: FormGroup;
  passwordMessage: string = '';
  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.buildFormGroupLogin();
    // alert('USUARIOS PARA AUTENTICARSE:\nUsuario: Byron, Contraseña: 123\nUsuario: Maria, Contraseña: MiContrasena\nUsuario: Evelyn, Contraseña: Eve123');
  }

  login(loginBody: Login, rol: string) {
    this.authService.login(loginBody, rol);
  }

  buildFormGroupLogin() {
    this.formGroupLogin = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
      rol: ['user', Validators.required],
    });
  }

  get userNameField() {
    return this.formGroupLogin.get('userName');
  }
  get passwordField() {
    return this.formGroupLogin.get('password');
  }

  onSubmit(event: Event) {
    event.preventDefault();
    if (this.formGroupLogin.valid) {
      this.login(
        this.formGroupLogin.getRawValue(),
        this.formGroupLogin.get('rol')?.value
      );
    }
  }
}

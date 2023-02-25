import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import Swal from 'sweetalert2';
import { AuthService } from './auth.service';
@Injectable({
    providedIn: 'root'
  })
export class AuthGuardService implements CanActivate {
  constructor(public auth: AuthService, public router: Router) {}
  //validar si el token ha expirado, de ser asi regresa al login
  canActivate(): boolean {
    if (!this.auth.isAuthenticated()) {
      Swal.fire({
        position: 'center',
        icon: 'warning',
        title: `El token ha caducado`,
        text: `Vuelve a iniciar sesión`,
        showConfirmButton: false,
        timer: 1500,
      });
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
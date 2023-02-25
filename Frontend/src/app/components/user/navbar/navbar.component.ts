import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';
import { EcommerceService } from 'src/app/services/ecommerce.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  constructor(private authService: AuthService, public ecommerceService : EcommerceService) {}
  ngOnInit(): void {
    this.ecommerceService.getQuantity();
  }
  logOut() {
    this.authService.logout();
  }
  
}

import { Routes } from '@angular/router';
import { AuthGuardService } from './auth/authGuard.service';
import { LoginComponent } from './components/login/login.component';

export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {
    path: '',
    children: [
      {
        path: 'admin',
        loadChildren: () =>
          import('./components/admin/admin.module').then((m) => m.AdminModule),
        // canActivate: [AuthGuardService],
      },
      {
        path: 'ecommerce',
        loadChildren: () =>
          import('./components/user/user.module').then((m) => m.UserModule),
        // canActivate: [AuthGuardService],
      },
    ],
  },
];

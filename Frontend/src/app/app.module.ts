import { InjectionToken, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ProductsComponent } from './components/user/products/products.component';
import { NavbarComponent } from './components/user/navbar/navbar.component';
import { UserComponent } from './components/user/user.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AdminComponent } from './components/admin/admin.component';
import { ProductFormComponent } from './components/admin/product-admin/product-form/product-form.component';
import { ProductListComponent } from './components/admin/product-admin/product-list/product-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MyCartComponent } from './components/user/my-cart/my-cart.component';
import { LoginComponent } from './components/login/login.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './app.routing';
import { ProductAdminComponent } from './components/admin/product-admin/product-admin.component';
import { BrandAdminComponent } from './components/admin/brand-admin/brand-admin.component';
import { BrandListComponent } from './components/admin/brand-admin/brand-list/brand-list.component';
import { BrandFormComponent } from './components/admin/brand-admin/brand-form/brand-form.component';
import { AuthService } from './auth/auth.service';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { LoadingComponent } from './components/shared/loading/loading.component';
import { ProductInfoComponent } from './components/user/product-info/product-info.component';
import { ProductTypesComponent } from './components/user/products/product-types/product-types.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AdminNavbarComponent } from './components/admin/admin-navbar/admin-navbar.component';
import { DeliveryMethodAdminComponent } from './components/admin/delivery-method-admin/delivery-method-admin.component';
import { DeliveryMethodFormComponent } from './components/admin/delivery-method-admin/delivery-method-form/delivery-method-form.component';
import { DeliveryMethodListComponent } from './components/admin/delivery-method-admin/delivery-method-list/delivery-method-list.component';
import { ProductTypeAdminComponent } from './components/admin/product-type-admin/product-type-admin.component';
import { ProductTypeFormComponent } from './components/admin/product-type-admin/product-type-form/product-type-form.component';
import { ProductTypeListComponent } from './components/admin/product-type-admin/product-type-list/product-type-list.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    NavbarComponent,
    ProductTypesComponent,
    UserComponent,
    AdminComponent,
    ProductFormComponent,
    ProductListComponent,
    ProductInfoComponent,
    MyCartComponent,
    LoginComponent,
    ProductAdminComponent,
    BrandAdminComponent,
    BrandListComponent,
    BrandFormComponent,
    LoadingComponent,
    AdminNavbarComponent,
    BrandAdminComponent,
    BrandFormComponent,
    BrandListComponent,
    DeliveryMethodAdminComponent,
    DeliveryMethodFormComponent,
    DeliveryMethodListComponent,
    ProductTypeAdminComponent,
    ProductTypeFormComponent,
    ProductTypeListComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes),
    ModalModule.forRoot(),
  ],
  providers: [
    AuthService,
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    JwtHelperService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

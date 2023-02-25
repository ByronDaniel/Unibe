import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from 'src/app/auth/authGuard.service';
import { AdminComponent } from '../../components/admin/admin.component';
import { BrandAdminComponent } from '../../components/admin/brand-admin/brand-admin.component';
import { ProductAdminComponent } from '../../components/admin/product-admin/product-admin.component';
import { ProductFormComponent } from '../../components/admin/product-admin/product-form/product-form.component';
import { ProductInfoComponent } from '../user/product-info/product-info.component';
import { BrandFormComponent } from './brand-admin/brand-form/brand-form.component';
import { DeliveryMethodAdminComponent } from './delivery-method-admin/delivery-method-admin.component';
import { DeliveryMethodFormComponent } from './delivery-method-admin/delivery-method-form/delivery-method-form.component';
import { ProductTypeAdminComponent } from './product-type-admin/product-type-admin.component';
import { ProductTypeFormComponent } from './product-type-admin/product-type-form/product-type-form.component';

const routes: Routes = [
  {
    path: '',
    component: AdminComponent,
    children: [
      { path: 'products', component: ProductAdminComponent },
      { path: 'product/:id', component: ProductInfoComponent },
      { path: 'products/edit/:id', component: ProductFormComponent },
      { path: 'products/create', component: ProductFormComponent },

      { path: 'brands', component: BrandAdminComponent },
      { path: 'brands/edit/:id', component: BrandFormComponent },
      { path: 'brands/create', component: BrandFormComponent },

      { path: 'product-types', component: ProductTypeAdminComponent },
      { path: 'product-types/edit/:id', component: ProductTypeFormComponent },
      { path: 'product-types/create', component: ProductTypeFormComponent },

      { path: 'delivery-methods', component: DeliveryMethodAdminComponent },
      {
        path: 'delivery-methods/edit/:id',
        component: DeliveryMethodFormComponent,
      },
      {
        path: 'delivery-methods/create',
        component: DeliveryMethodFormComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}

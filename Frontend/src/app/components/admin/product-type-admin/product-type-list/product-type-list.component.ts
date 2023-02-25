import { Component, OnInit } from '@angular/core';
import { ProductType } from 'src/app/models/ProductType';
import { EcommerceService } from 'src/app/services/ecommerce.service';
import { LoaderService } from 'src/app/services/loader.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-product-type-list',
  templateUrl: './product-type-list.component.html',
  styleUrls: ['./product-type-list.component.css'],
})
export class ProductTypeListComponent implements OnInit {
  productTypes: ProductType[] = [];
  constructor(
    private ecommerceService: EcommerceService,
    private loaderService: LoaderService
  ) {}

  ngOnInit(): void {
    this.getProductTypes();
  }

  getProductTypes() {
    this.loaderService.loaderState();
    this.ecommerceService
      .get('ProductType?limit=0&offset=0&sort=Name&order=asc')
      .subscribe((response) => {
        this.productTypes = response as ProductType[];
        this.loaderService.loaderState(false);
      });
  }

  deleteProductType(productTypeId: string) {
    this.loaderService.loaderState();
    const swalWithBootstrapButtons = Swal.mixin({
      buttonsStyling: true,
    });

    swalWithBootstrapButtons
      .fire({
        title: 'Eliminar',
        text: 'Estás Seguro de eliminar la categoria?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true,
      })
      .then((result) => {
        this.ecommerceService
          .delete(`ProductType/${productTypeId}`)
          .subscribe((response) => {
            this.productTypes = this.productTypes.filter(
              (productType) => productType.id != productTypeId
            );
            this.loaderService.loaderState(false);
            if (result.isConfirmed) {
              swalWithBootstrapButtons.fire(
                'Eliminado!',
                'Categoria Eliminada con Exíto',
                'success'
              );
            }
          });
      });
  }

  productTypeNewOut(productType: ProductType) {
    this.productTypes.push(productType);
  }
}

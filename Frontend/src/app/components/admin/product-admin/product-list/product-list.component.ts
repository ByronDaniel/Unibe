import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/Product';
import { EcommerceService } from 'src/app/services/ecommerce.service';
import { LoaderService } from 'src/app/services/loader.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  constructor(
    private ecommerceService: EcommerceService,
    private loaderService: LoaderService
  ) {}

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.loaderService.loaderState();
    this.ecommerceService
      .get('Product?limit=0&offset=0&sort=Name&order=asc')
      .subscribe((response) => {
        this.products = response as Product[];
        this.loaderService.loaderState(false);
      });
  }

  deleteProduct(productId: string) {
    this.loaderService.loaderState();
    const swalWithBootstrapButtons = Swal.mixin({
      buttonsStyling: true,
    });

    swalWithBootstrapButtons
      .fire({
        title: 'Eliminar',
        text: 'Estás Seguro de eliminar el producto?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true,
      })
      .then((result) => {
        this.ecommerceService
          .delete(`Product/${productId}`)
          .subscribe((response) => {
            this.products = this.products.filter(
              (product) => product.id != productId
            );
            this.loaderService.loaderState(false);
            if (result.isConfirmed) {
              swalWithBootstrapButtons.fire(
                'Eliminado!',
                'Producto Eliminado con Exíto',
                'success'
              );
            }
          });
      });
  }

  productNewOut(product: Product) {
    this.products.push(product);
  }
}

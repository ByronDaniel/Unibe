import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Product } from 'src/app/models/Product';
import { ProductType } from 'src/app/models/ProductType';
import { EcommerceService } from 'src/app/services/ecommerce.service';
import { LoaderService } from 'src/app/services/loader.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-product-types',
  templateUrl: './product-types.component.html',
  styleUrls: ['./product-types.component.css'],
})
export class ProductTypesComponent implements OnInit {
  productTypes: ProductType[] = [];
  products: Product[] = [];
  productTypeSelected: string = '';
  searchProduct: string = '';
  @Output() productsOut = new EventEmitter<Product[]>();
  @Output() productTypeSelectedOut = new EventEmitter<string>();
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
      .get('ProductType?sort=Name&order=Asc&limit=0&offset=0')
      .subscribe((response) => {
        this.productTypes = response as ProductType[];
        if(this.productTypes.length > 0){
          this.getProducts(this.productTypes[0]);
        }
        else{
          Swal.fire({
            position: 'center',
            icon: 'warning',
            title: `No hay categorias disponibles`,
            showConfirmButton: false,
            timer: 1500
          });
        }
        this.loaderService.loaderState(false);
      });
  }

  getProducts(productType: ProductType) {
    this.loaderService.loaderState();
    this.ecommerceService
      .get(
        `Product?search=${productType.id}&sort=Name&order=Asc&limit=0&offset=0`
      )
      .subscribe((response) => {
        this.products = response as Product[];
        this.productTypeSelected = productType.name;
        this.productTypeSelectedOut.emit(productType.name);
        this.productsOut.emit(this.products);
        this.loaderService.loaderState(false);
      });
  }

  searchProducts() {
    if (this.searchProduct != '') {
      this.ecommerceService
        .get(
          `Product?search=${this.searchProduct}&sort=Name&order=Asc&limit=0&offset=0`
        )
        .subscribe((response) => {
          this.products = response as Product[];
          this.productTypeSelected = `Busqueda: ${this.searchProduct}`;
          this.productTypeSelectedOut.emit(this.productTypeSelected);
          this.productsOut.emit(this.products);
        });
    } else {
      this.getProductTypes();
    }
  }
}

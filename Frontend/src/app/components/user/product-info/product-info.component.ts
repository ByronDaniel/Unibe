import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Product } from 'src/app/models/Product';
import { ActivatedRoute, Router } from '@angular/router';
import { EcommerceService } from 'src/app/services/ecommerce.service';
import { Order } from 'src/app/models/Order';
import { OrderProductDto } from 'src/app/models/OrderProductDto';
import Swal from 'sweetalert2';
import { Location } from '@angular/common';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.css'],
})
export class ProductInfoComponent implements OnInit {
  @Input() productSelected!: Product;
  @Output() closeModal = new EventEmitter();
  stock: number[] = [];
  addProduct: OrderProductDto = {
    productId: '',
    productQuantity: 1,
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private ecommerceService: EcommerceService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.getProductInfo(this.productSelected.id!);
  }

  getProductInfo(id: string) {
    this.ecommerceService.get(`Product/${id}`).subscribe((response) => {
      this.productSelected = response as Product;
      this.getStock(this.productSelected.stock);
    });
  }

  getStock(stock: number): void {
    for (let i = 1; i <= stock; i++) {
      this.stock.push(i);
    }
  }

  addProductCart(productSelectedId: string, quantity: string) {
    this.addProduct.productId = productSelectedId;
    this.addProduct.productQuantity = parseInt(quantity);
    let orderId = localStorage.getItem('orderId');

    if (orderId == null) {
      this.ecommerceService.post('Order').subscribe((response) => {
        let responseOrder = response as Order;
        let orderNew = responseOrder.id;
        localStorage.setItem('orderId', orderNew);

        this.ecommerceService
          .post(`Order/${orderNew}/Product`, this.addProduct)
          .subscribe((response) => {
            Swal.fire({
              position: 'center',
              icon: 'success',
              title: 'Agregado al Carrito',
              showConfirmButton: false,
              timer: 1000,
            });
            this.ecommerceService.getQuantity();
          });
        this.closeModal.emit(true);
      });
    } else {
      this.ecommerceService
        .post(`Order/${orderId}/Product`, this.addProduct)
        .subscribe((response) => {
          Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Agregado al Carrito',
            showConfirmButton: false,
            timer: 1000,
          });
          this.ecommerceService.getQuantity();
          this.closeModal.emit(true);
        });
    }
  }
}

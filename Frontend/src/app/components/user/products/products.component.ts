import { Component, OnInit, TemplateRef } from '@angular/core';
import { Product } from 'src/app/models/Product';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

import Swal from 'sweetalert2';
@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit {
  modalRef?: BsModalRef;
  productSelected!: Product;
  constructor(private modalService: BsModalService) {}
  openModal(template: TemplateRef<any>, product: Product) {
    this.modalRef = this.modalService.show(
      template,
      Object.assign({}, { class: 'gray modal-lg' })
    );
    this.productSelected = product;
  }

  ngOnInit(): void {
    
  }
  products: Product[] = [];
  productTypeSelected: string = '';

  productsOut(products: Product[]) {
    this.products = products;
  }

  productTypeSelectedOut(name: string) {
    this.productTypeSelected = name;
  }

  closeModal() {
    this.modalRef?.hide();
  }
}

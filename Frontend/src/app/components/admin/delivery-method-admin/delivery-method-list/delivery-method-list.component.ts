import { Component, OnInit } from '@angular/core';
import { DeliveryMethod } from 'src/app/models/DeliveryMethod';
import { EcommerceService } from 'src/app/services/ecommerce.service';
import { LoaderService } from 'src/app/services/loader.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-delivery-method-list',
  templateUrl: './delivery-method-list.component.html',
  styleUrls: ['./delivery-method-list.component.css'],
})
export class DeliveryMethodListComponent implements OnInit {
  deliveryMethods: DeliveryMethod[] = [];
  constructor(
    private ecommerceService: EcommerceService,
    private loaderService: LoaderService
  ) {}

  ngOnInit(): void {
    this.getDeliveryMethods();
  }

  getDeliveryMethods() {
    this.loaderService.loaderState();
    this.ecommerceService
      .get('DeliveryMethod?limit=0&offset=0&sort=Name&order=asc')
      .subscribe((response) => {
        this.deliveryMethods = response as DeliveryMethod[];
        this.loaderService.loaderState(false);
      });
  }

  deleteDeliveryMethod(deliveryMethodId: string) {
    this.loaderService.loaderState();
    const swalWithBootstrapButtons = Swal.mixin({
      buttonsStyling: true,
    });

    swalWithBootstrapButtons
      .fire({
        title: 'Eliminar',
        text: 'Estás Seguro de eliminar el Método de Entrega?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true,
      })
      .then((result) => {
        this.ecommerceService
          .delete(`DeliveryMethod/${deliveryMethodId}`)
          .subscribe((response) => {
            this.deliveryMethods = this.deliveryMethods.filter(
              (deliveryMethod) => deliveryMethod.id != deliveryMethodId
            );
            this.loaderService.loaderState(false);
            if (result.isConfirmed) {
              swalWithBootstrapButtons.fire(
                'Eliminado!',
                'Método de Entrega Eliminado con Exíto',
                'success'
              );
            }
          });
      });
  }

  deliveryMethodNewOut(deliveryMethod: DeliveryMethod) {
    this.deliveryMethods.push(deliveryMethod);
  }
}

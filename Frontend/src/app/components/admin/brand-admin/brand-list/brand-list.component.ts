import { Component, OnInit } from '@angular/core';
import { Brand } from 'src/app/models/Brand';
import { EcommerceService } from 'src/app/services/ecommerce.service';
import { LoaderService } from 'src/app/services/loader.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-brand-list',
  templateUrl: './brand-list.component.html',
  styleUrls: ['./brand-list.component.css'],
})
export class BrandListComponent implements OnInit {
  brands: Brand[] = [];
  constructor(
    private ecommerceService: EcommerceService,
    private loaderService: LoaderService
  ) {}

  ngOnInit(): void {
    this.getBrands();
  }

  //Obtiene marcas ejecutando el servicio ecommerce
  getBrands() {
    this.loaderService.loaderState();
    this.ecommerceService
      .get('Brand?limit=0&offset=0&sort=Name&order=asc')
      .subscribe((response) => {
        this.brands = response as Brand[];
        this.loaderService.loaderState(false);
      });
  }

  //Elimina la marca enviando el id
  deleteBrand(brandId: string) {
    this.loaderService.loaderState();
    const swalWithBootstrapButtons = Swal.mixin({
      buttonsStyling: true,
    });
    //Modal para confirmar eliminado
    swalWithBootstrapButtons
      .fire({
        title: 'Eliminar',
        text: 'Estás Seguro de eliminar la marca?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true,
      })
      .then((result) => {
        this.ecommerceService
          .delete(`Brand/${brandId}`)
          .subscribe((response) => {
            this.brands = this.brands.filter((brand) => brand.id != brandId);
            this.loaderService.loaderState(false);
            if (result.isConfirmed) {
              swalWithBootstrapButtons.fire(
                'Eliminado!',
                'Marca Eliminada con Exíto',
                'success'
              );
            }
          });
      });
  }

  brandNewOut(brand: Brand) {
    this.brands.push(brand);
  }
}

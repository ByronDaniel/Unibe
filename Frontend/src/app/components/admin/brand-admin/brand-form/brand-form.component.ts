import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLinkActive } from '@angular/router';
import { Brand } from 'src/app/models/Brand';
import { EcommerceService } from 'src/app/services/ecommerce.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-brand-form',
  templateUrl: './brand-form.component.html',
  styleUrls: ['./brand-form.component.css'],
})
export class BrandFormComponent implements OnInit {
  formGroupBrand!: FormGroup;
  brand!: Brand;
  brands: Brand[] = [];
  id!: string;
  @Output() brandNewOut = new EventEmitter();
  constructor(
    private ecommerceService: EcommerceService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.buildformGroupBrand();
  }
  
  //Agrega marca llamando al servicio ecommerce y emite la marca creada
  addBrand(brand: Brand) {
    this.ecommerceService
      .post('Brand', this.formGroupBrand.getRawValue())
      .subscribe((response) => {
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: `Marca Agregada con Éxito!`,
          showConfirmButton: false,
          timer: 1500,
        });
        this.router.navigate(['/admin/brands']);
        this.brandNewOut.emit(brand);
      });
  }

  //Reactive Form, Validators and getters 
  buildformGroupBrand() {
    this.id = this.route.snapshot.paramMap.get('id')!;
    if (!this.id) {
      this.formGroupBrand = this.formBuilder.group({
        name: [null, [Validators.required]],
      });
    } else {
      this.getBrand(this.id);
    }
  }

  //Getters para validaciones desde html
  get nameField() {
    return this.formGroupBrand.get('name');
  }
  get idField() {
    return this.formGroupBrand.get('id');
  }

  //Valida si existe el id para editar, caso contrario ejecuta metodo para crear nuevo registro
  onSubmit(event: Event) {
    event.preventDefault();
    if (this.id != null) {
      this.updateBrand();
    } else {
      if (this.formGroupBrand.valid) {
        this.addBrand(this.formGroupBrand.value);
      }
    }
  }

  //Obtiene las marcas llamando al servio ecommerce
  getBrand(id: string) {
    this.ecommerceService.get(`Brand/${id}`).subscribe((response) => {
      this.brand = response as Brand;
      this.formGroupBrand = this.formBuilder.group({
        id: [this.brand.id, [Validators.required]],
        name: [this.brand.name, [Validators.required]],
      });
    });
  }

  //Actualiza Marca
  updateBrand() {
    this.ecommerceService
      .put(`Brand`, this.formGroupBrand.getRawValue())
      .subscribe((response) => {
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: `Marca Actualizada con Éxito!`,
          showConfirmButton: false,
          timer: 1500,
        });
        this.router.navigate(['/admin/brands']);
      });
  }
}

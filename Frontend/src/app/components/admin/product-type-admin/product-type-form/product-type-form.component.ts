import { ThisReceiver } from '@angular/compiler';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLinkActive } from '@angular/router';
import { Brand } from 'src/app/models/Brand';
import { ProductType } from 'src/app/models/ProductType';
import { EcommerceService } from 'src/app/services/ecommerce.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-product-type-form',
  templateUrl: './product-type-form.component.html',
  styleUrls: ['./product-type-form.component.css']
})
export class ProductTypeFormComponent implements OnInit {
  formGroupProductType!: FormGroup;
  productType!: ProductType;
  productTypes : ProductType [] = [];
  brands : Brand [] = [];
  productTypeId!: string;
  @Output() productTypeNewOut = new EventEmitter();
  constructor(private ecommerceService : EcommerceService, private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router) { 
  }

  ngOnInit(): void {
    this.buildformGroupProductType();
    this.getProductTypes();
  }

  getProductTypes(){
    this.ecommerceService.get('ProductType?sort=Name&order=Asc&offset=0').subscribe(response =>{
      this.productTypes = response as ProductType[];
    });
  }

  addProductType(productType: ProductType){
    this.ecommerceService.post('ProductType',this.formGroupProductType.getRawValue()).subscribe(response =>{
      Swal.fire({
        position: 'center',
        icon: 'success',
        title: `Categoria Agregada con Éxito!`,
        showConfirmButton: false,
        timer: 1500
      });
      this.router.navigate(['/admin/product-types']);
      this.productTypeNewOut.emit(productType);
    });
  }
   
  //Reactive Form, Validators and getters of Fields
  buildformGroupProductType(){
    this.productTypeId = this.route.snapshot.paramMap.get('id')!;
    if(!this.productTypeId){
      this.formGroupProductType = this.formBuilder.group({
        name: [null,[Validators.required]],
      });
    }else{
      this.getProductType(this.productTypeId);
    }
  }

  get nameField() {
    return this.formGroupProductType.get('name');
  }
  get idField() {
    return this.formGroupProductType.get('id');
  }
  
  onSubmit(event : Event){
    event.preventDefault();
    if(this.productTypeId != null ){
      this.updateProductType();
    }else{
      if(this.formGroupProductType.valid){
        this.addProductType(this.formGroupProductType.value);
      }
    }
  }

  getProductType(id: string){
    this.ecommerceService.get(`ProductType/${id}`).subscribe(
      response =>{
        this.productType = response as ProductType;
        this.formGroupProductType = this.formBuilder.group({
          id: [this.productType.id,[Validators.required]],
          name: [this.productType.name,[Validators.required]],
        });
      }
    )
  }

  updateProductType(){
    this.ecommerceService.put(`ProductType`,this.formGroupProductType.getRawValue()).subscribe(
      response=>{
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: `Categoria Actualizada con Éxito!`,
          showConfirmButton: false,
          timer: 1500
        });
        this.router.navigate(['/admin/product-types']);
      }
    );
  }
}


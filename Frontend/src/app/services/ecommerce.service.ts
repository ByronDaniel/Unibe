import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class EcommerceService {
  API_URL: string = environment.API_URL;
  token: string = '';
  quantity: number = 0;
  constructor(
    private httpClient: HttpClient
  ) {}

  get(url: string, params = new HttpParams()) {
    url = this.API_URL + url;
    return this.httpClient.get(url, { params });
  }

  post(url: string, data?: any, params = new HttpParams()) {
    url = this.API_URL + url;
    return this.httpClient.post(url, data, { params });
  }

  put(url: string, data?: any, params = new HttpParams()) {
    url = this.API_URL + url;
    return this.httpClient.put(url, data, { params });
  }

  delete(url: string, params = new HttpParams()) {
    url = this.API_URL + url;
    return this.httpClient.delete(url, { params });
  }

  getQuantity(){
    let orderId = localStorage.getItem('orderId');
    if(orderId != null){
      this.get(`Order/Quantity-products?orderId=${orderId}`).subscribe( response=>{
        this.quantity = response as number
      }
    )}else{
      this.quantity = 0;
    }
    }
}

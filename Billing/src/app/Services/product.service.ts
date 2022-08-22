import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from '../Models/Product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private _http:HttpClient) { }

  getProducts(): Observable<any[]> {
    return this._http.get<any>(`${environment.baseUrl}${environment.apiUrlProducts}`);
  }

  addProduct(product: Product): Observable<Product> {
    return this._http.post<Product>(`${environment.baseUrl}${environment.apiUrlProducts}`, product);
  }

  updateProduct(product: Product): Observable<Product> {
    return this._http.put<Product>(`${environment.baseUrl}${environment.apiUrlProducts}`, product);
  }



  delProduct(product: Product): Observable<Product> {
    return this._http.delete<Product>(`${environment.baseUrl}${environment.apiUrlProducts}/${product.idProduct}`);
  }

}

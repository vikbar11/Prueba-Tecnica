import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Customer } from '../Models/Customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private _http:HttpClient) { }

  getCustomers(): Observable<any> {
    return this._http.get(`${environment.baseUrl}${environment.apiUrlCustomers}`);
  }

  addCustomer(customer: Customer): Observable<Customer> {
    return this._http.post<Customer>(`${environment.baseUrl}${environment.apiUrlCustomers}`, customer);
  }

  updateCustomer(customer: Customer): Observable<Customer> {
    return this._http.put<Customer>(`${environment.baseUrl}${environment.apiUrlCustomers}/${customer.idCustomer}`, customer);
  }


  delCustomer(customer: Customer): Observable<Customer> {
    return this._http.delete<Customer>(`${environment.baseUrl}${environment.apiUrlCustomers}/${customer.idCustomer}`);
  }

}

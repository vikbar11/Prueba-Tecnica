import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GetBill, GetBillDetails, SaveBill } from '../Models/Bill';
import { BillDetail } from '../Models/BillDetail';

@Injectable({
  providedIn: 'root'
})
export class BillService {

  constructor(private _http:HttpClient) { }

  getBills(): Observable<any> {
    return this._http.get(`${environment.baseUrl}${environment.apiUrlBill}`);
  }

  addBill(bill: SaveBill): Observable<GetBill>{
    return this._http.post<GetBill>(`${environment.baseUrl}${environment.apiUrlBill}`, bill);
  }

  //l
  getFBillDetail(idBill: number): Observable<BillDetail> {
    return this._http.get<BillDetail>(`${environment.baseUrl}${environment.apiUrlBillDetail}${idBill}`);
  }

  updateBillDetail(detail: BillDetail): Observable<BillDetail> {
    return this._http.put<BillDetail>(`${environment.baseUrl}${environment.apiUrlBillDetail}`, detail);
  }


  getBilldetail(id: number){
    return this._http.get<GetBillDetails[]>(`${environment.baseUrl}${environment.apiUrlBill}/${id}`);
  }

  addBillDetail(detail: BillDetail): Observable<GetBill>{
    return this._http.post<GetBill>(`${environment.baseUrl}${environment.apiUrlBillDetail}`, detail);
  }

}

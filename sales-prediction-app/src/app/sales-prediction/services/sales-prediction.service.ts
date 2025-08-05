
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiResponse, CustomerPrediction, Order } from '../interfaces/sales-prediction.interfaces';

@Injectable({
  providedIn: 'root'
})
export class SalesPredictionService {

  private apiUrl = 'https://localhost:5001/api';

  constructor(private http: HttpClient) {}

  getCustomerPredictions(pageIndex: number, pageSize: number, sortField: string, sortOrder: 'asc' | 'desc', filterValue: string): Observable<ApiResponse<CustomerPrediction>> {
    let params = new HttpParams()
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString())
      .set('sortField', sortField)
      .set('sortOrder', sortOrder)
      .set('filterValue', filterValue);

    return this.http.get<ApiResponse<CustomerPrediction>>(`${this.apiUrl}/CustomerOrder/salespredictions`, { params });
  }

  getOrdersByCustomerId(custId: number): Observable<Order[]> {
    return this.http.get<{ data: Order[] }>(`${this.apiUrl}/Order/customer/${custId}`)
      .pipe(
        map(response => response.data)
      );
  }
  
  createOrder(orderData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/order`, orderData);
  }
}

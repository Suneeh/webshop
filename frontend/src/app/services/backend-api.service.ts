import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment as env } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ExternalApiService {
  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${env.api.url}/products/`);
  }
}

export interface Product {
  Id: number;
  Name: string;
  Description: string;
  CreationDate: Date;
  ChangedDate: Date;
  NetPrice: number;
  TaxRate: number;
}

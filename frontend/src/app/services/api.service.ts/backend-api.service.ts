import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUri } from '../../../environments/environment';
import { GetCategoryDto } from './dtos/categories/get-category-dto';
import { PutCategoryDto } from './dtos/categories/put-category-dto';
import { GetProductDto } from './dtos/products/get-product-dto';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private http = inject(HttpClient)
  public headers: HeadersInit | undefined;

  getProducts(): Observable<GetProductDto[]> {
    return this.http.get<GetProductDto[]>(`${apiUri}/products/`);
  }

  getCategories() {
    return this.http.get<GetCategoryDto[]>(`${apiUri}/categories/`);
  }

  getCategory(id: number) {
    return this.http.get<GetCategoryDto>(`${apiUri}/categories/${id}`);
  }

  putCategoryResource(category: PutCategoryDto) {
    return fetch(`${apiUri}/categories/`, {
      method: "PUT",
      headers: this.headers, 
      /**
       * TODO - those are never being set - Need to look something like this 
      {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
       */
      body: JSON.stringify(category)
    });
  }
}

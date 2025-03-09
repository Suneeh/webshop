import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUri } from '../../../environments/environment';
import { GetCategoryDetailDto } from './dtos/categories/get-category-detail-dto';
import { PutCategoryDto } from './dtos/categories/put-category-dto';
import { GetProductDto } from './dtos/products/get-product-dto';
import { GetCategoryListDto } from './dtos/categories/get-category-list-dto';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private http = inject(HttpClient);

  getProducts(): Observable<GetProductDto[]> {
    return this.http.get<GetProductDto[]>(`${apiUri}/products/`);
  }

  getProduct(id: string): Observable<GetProductDto> {
    return this.http.get<GetProductDto>(`${apiUri}/products/${id}`);
  }

  getCategories() {
    return this.http.get<GetCategoryListDto[]>(`${apiUri}/categories/`);
  }

  getCategory(id: string) {
    return this.http.get<GetCategoryDetailDto>(`${apiUri}/categories/${id}`);
  }

  putCategory(category: PutCategoryDto) {
    return this.http.put(`${apiUri}/categories/`, category);
  }
}

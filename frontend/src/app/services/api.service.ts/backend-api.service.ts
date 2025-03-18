import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { apiUri } from '../../../environments/environment';
import { GetCategoryDetailDto } from './dtos/categories/get-category-detail-dto';
import { PutCategoryDto } from './dtos/categories/put-category-dto';
import { GetProductDetailDto } from './dtos/products/get-product-detail-dto';
import { GetCategoryListDto } from './dtos/categories/get-category-list-dto';
import { GetProductListDto } from './dtos/products/get-product-list-dto';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private http = inject(HttpClient);

  getProducts(data: GetProductListQueryParams): Observable<GetProductListDto[]> {
    return this.http.get<GetProductListDto[]>(`${apiUri}/products?${toQueryString(data)}`);
  }

  getProduct(id: string): Observable<GetProductDetailDto> {
    return this.http.get<GetProductDetailDto>(`${apiUri}/products/${id}`);
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

interface GetProductListQueryParams {
  skip?: number;
  take?: number;
  sortBy?: string;
  sortOrder?: string;
}

const toQueryString = <T extends { hasOwnProperty: (k: string) => boolean }>(queryParameter: T) => {
  const parts: string[] = [];
  for (const key in queryParameter) {
    if (!Object.prototype.hasOwnProperty.call(queryParameter, key)) {
      continue;
    }

    const value = queryParameter[key];
    if (value === null || value === undefined) {
      parts.push(key + '=');
    } else if (value instanceof Date) {
      parts.push(key + '=' + encodeURIComponent(value.toISOString()));
    } else if (typeof value === 'number' || typeof value === 'boolean') {
      parts.push(key + '=' + value);
    } else if (typeof value === 'string') {
      parts.push(key + '=' + encodeURIComponent(value));
    } else if (Array.isArray(value)) {
      const arrayQueryString = arrayToQueryString(key, value);
      if (arrayQueryString !== '') {
        parts.push(arrayQueryString);
      }
    } else {
      throw new Error('cannot create querystring for value: ' + JSON.stringify(value));
    }
  }

  return parts.join('&');
};

const arrayToQueryString = (paramName: string, items: (string | number | boolean)[]) => {
  if (!items || items.length === 0) {
    return '';
  }

  return paramName + '=' + items.map((item) => encodeURIComponent(item)).join('&' + paramName + '=');
};

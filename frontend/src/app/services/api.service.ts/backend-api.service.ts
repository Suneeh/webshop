import { HttpClient } from '@angular/common/http';
import { inject, Injectable, resource } from '@angular/core';
import { apiUri, environment as env } from '../../../environments/environment';
import { Observable, tap } from 'rxjs';
import { AuthService } from '@auth0/auth0-angular';
import { GetCategoryDto } from './dtos/categories/get-category-dto';
import { PutCategoryDto } from './dtos/categories/put-category-dto';
import { GetProductDto } from './dtos/products/get-product-dto';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private auth = inject(AuthService);
  private http = inject(HttpClient)
  private headers: HeadersInit | undefined;

  constructor() {
    this.auth.getAccessTokenSilently().pipe(tap(token => {
      if (token) {
        this.headers = {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        }
      }
      else {
        this.headers = {
          'Content-Type': 'application/json'
        }
      }
    })).subscribe();
  }

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
    return resource({
      loader: () => {
        return fetch(`${apiUri}/categories/`, {
          method: "PUT",
          headers: this.headers,
          body: JSON.stringify(category)
        });
      },
    });
  }
}





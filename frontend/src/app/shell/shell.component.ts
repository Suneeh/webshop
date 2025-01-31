import { Component, ChangeDetectionStrategy } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterModule } from '@angular/router';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Observable, map, of } from 'rxjs';
import { CommonModule } from '@angular/common';
import { AuthService, User } from '@auth0/auth0-angular';
import { MatButtonModule } from '@angular/material/button';
import { ApiService } from '../services/api.service.ts/backend-api.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-shell',
  imports: [MatSidenavModule, RouterModule, MatListModule, MatButtonModule, MatIconModule, MatToolbarModule, CommonModule],
  templateUrl: './shell.component.html',
  styleUrl: './shell.component.scss'
})
export class ShellComponent {
  public categories = new Observable<Category[]>();
  year = new Date().getFullYear();
  constructor(public auth: AuthService, public apiService: ApiService) {
    this.categories = of([
      { id: 1, name: '1st Category', childCategories: [] },
      { id: 2, name: '2nd Category', childCategories: [] },
      {
        id: 3,
        name: '3rd Category',
        childCategories: [
          { id: 4, name: '4th Category', childCategories: [] },
          { id: 5, name: '4th Category', childCategories: [] },
        ],
      },
    ]);
  }

  loginWithRedirect() {
    this.auth.loginWithRedirect({ authorizationParams: { scope: 'manage' } });
  }

  logout() {
    this.auth.logout({ logoutParams: { returnTo: window.location.origin } });
  }

  logUser(user: User) {
    this.auth
      .getAccessTokenSilently()
      .pipe(
        map((token) => {
          return console.log(token);
        })
      )
      .subscribe();
    console.log(user);

    this.apiService
      .getProducts()
      .pipe(
        map((products) => {
          return console.log(products);
        })
      )
      .subscribe();
  }
}

interface Category {
  id: number;
  name: string;
  childCategories: Category[];
}

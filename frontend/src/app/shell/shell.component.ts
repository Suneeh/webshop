import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { Observable, of } from 'rxjs';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-shell',
  imports: [MatSidenavModule, RouterModule, MatListModule, MatButtonModule, MatIconModule, MatToolbarModule, CommonModule],
  templateUrl: './shell.component.html',
  styleUrl: './shell.component.scss'
})
export class ShellComponent {
  public auth = inject(AuthService);
  public categories = new Observable<Category[]>();
  year = new Date().getFullYear();
  constructor() {
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
}

interface Category {
  id: number;
  name: string;
  childCategories: Category[];
}

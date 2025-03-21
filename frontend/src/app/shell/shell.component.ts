import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { ZvView, ZvViewDataSource } from '@zvoove/components/view';
import { of } from 'rxjs';
import { ApiService } from '../services/api.service.ts/backend-api.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-shell',
  imports: [MatSidenavModule, RouterModule, MatListModule, MatButtonModule, MatIconModule, MatToolbarModule, CommonModule, ZvView],
  templateUrl: './shell.component.html',
  styleUrl: './shell.component.scss',
})
export class ShellComponent {
  public auth = inject(AuthService);
  public api = inject(ApiService);
  year = new Date().getFullYear();

  ds = new ZvViewDataSource({
    loadTrigger$: of({}),
    loadFn: () => this.api.getCategories(),
  });

  loginWithRedirect() {
    this.auth.loginWithRedirect({ authorizationParams: { scope: 'manage use' } });
  }

  logout() {
    this.auth.logout({ logoutParams: { returnTo: window.location.origin } });
  }
}

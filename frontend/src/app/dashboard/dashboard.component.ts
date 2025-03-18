import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ApiService } from '../services/api.service.ts/backend-api.service';
import { ZvView, ZvViewDataSource } from '@zvoove/components/view';
import { of } from 'rxjs';
import { ProductListComponent } from '../product/product-list/product-list.component';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-dashboard',
  imports: [CommonModule, ZvView, ProductListComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {
  api = inject(ApiService);

  latestProductsDs = new ZvViewDataSource({
    loadTrigger$: of({}),
    loadFn: () => this.api.getProducts({ skip: 0, take: 4, sortBy: 'CreationDate', sortOrder: 'desc' }),
  });
}

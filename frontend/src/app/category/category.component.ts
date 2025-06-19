import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ZvView, ZvViewDataSource } from '@zvoove/components/view';
import { ApiService } from '../services/api.service.ts/backend-api.service';
import { ProductListComponent } from '../product/product-list/product-list.component';
import { coerceNumberProperty } from '@angular/cdk/coercion';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-category',
  imports: [CommonModule, ZvView, ProductListComponent],
  templateUrl: './category.component.html',
})
export class CategoryComponent {
  private route = inject(ActivatedRoute);
  private api = inject(ApiService);

  ds = new ZvViewDataSource({
    loadTrigger$: this.route.paramMap,
    loadFn: (paramMap) => this.api.getCategory(coerceNumberProperty(paramMap.get('id'))),
  });
}

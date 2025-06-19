import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute } from '@angular/router';
import { ZvView, ZvViewDataSource } from '@zvoove/components/view';
import { ApiService } from '../../services/api.service.ts/backend-api.service';
import { ProductRatingComponent } from '../product-rating/product-rating.component';
import { coerceNumberProperty } from '@angular/cdk/coercion';
@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-product-detail',
  imports: [CommonModule, ZvView, MatIconModule, ProductRatingComponent],
  templateUrl: './product-detail.component.html',
})
export class ProductDetailComponent {
  private route = inject(ActivatedRoute);
  private api = inject(ApiService);
  ds = new ZvViewDataSource({
    loadTrigger$: this.route.paramMap,
    loadFn: (paramMap) => this.api.getProduct(coerceNumberProperty(paramMap.get('id'))),
  });
}

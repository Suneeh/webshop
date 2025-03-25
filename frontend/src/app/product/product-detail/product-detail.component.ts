import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { ZvView, ZvViewDataSource } from '@zvoove/components/view';
import { ApiService } from '../../services/api.service.ts/backend-api.service';
@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-product-detail',
  imports: [CommonModule, ZvView, MatIconModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss',
})
export class ProductDetailComponent {
  private route = inject(ActivatedRoute);
  private api = inject(ApiService);
  private auth = inject(AuthService);

  private userId = toSignal(this.auth.user$);
  ds = new ZvViewDataSource({
    loadTrigger$: this.route.paramMap,
    loadFn: (paramMap) => this.api.getProduct(paramMap.get('id') ?? ''),
  });

  rateItem(rating: number) {
    // Convert This To ActionDataSource !!
    console.log(rating);
    console.log(this.userId()?.email); // if this is null | undefined -> user needs to login first becuse there is no anonymous rating here!
    console.log(this.ds.result()?.id);
  }
}

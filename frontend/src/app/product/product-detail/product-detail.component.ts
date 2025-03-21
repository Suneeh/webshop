import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ZvView, ZvViewDataSource } from '@zvoove/components/view';
import { ApiService } from '../../services/api.service.ts/backend-api.service';
import { AuthService } from '@auth0/auth0-angular';
import { toSignal } from '@angular/core/rxjs-interop';
import { MatIconModule } from '@angular/material/icon';
import { of } from 'rxjs';
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

  dsfoo = new ZvViewDataSource({
    loadTrigger$: of({}),
    loadFn: () => this.api.getFoo(),
  });

  rateItem(rating: number) {
    console.log(rating);
    console.log(this.dsfoo.result());
    console.log(this.userId()?.email); // if this is null | undefined -> user needs to login first becuse there is no anonymous rating here!
    console.log(this.ds.result()?.id);
  }
}

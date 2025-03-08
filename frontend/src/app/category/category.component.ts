import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ZvView, ZvViewDataSource } from '@zvoove/components/view';
import { ZvCard } from '@zvoove/components/card';
import { ApiService } from '../services/api.service.ts/backend-api.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-category',
  imports: [CommonModule, ZvView, ZvCard],
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss',
})
export class CategoryComponent {
  private route = inject(ActivatedRoute);
  private api = inject(ApiService);

  ds = new ZvViewDataSource({
    loadTrigger$: this.route.paramMap,
    loadFn: (paramMap) => this.api.getCategory(+(paramMap.get('id') ?? '')),
  });
}

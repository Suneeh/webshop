import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs';
import { ApiService } from '../services/api.service.ts/backend-api.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-category',
  imports: [CommonModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss'
})
export class CategoryComponent {
  private route: ActivatedRoute = inject(ActivatedRoute);
  private api = inject(ApiService);
  public category$ = this.route.paramMap.pipe(
    switchMap((params) => this.api.getCategory(+(params.get('id') ?? '')))
  );
}

import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-category',
    imports: [CommonModule],
    templateUrl: './category.component.html',
    styleUrl: './category.component.scss'
})
export class CategoryComponent {
  route: ActivatedRoute = inject(ActivatedRoute);
  categoryId = -1;
  constructor() {
    this.categoryId = Number(this.route.snapshot.params['id']);
  }
}

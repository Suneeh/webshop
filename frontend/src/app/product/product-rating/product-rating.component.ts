import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, computed, inject, input } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-product-rating',
  imports: [CommonModule, MatIconModule],
  templateUrl: './product-rating.component.html',
  styleUrl: './product-rating.component.scss',
})
export class ProductRatingComponent {
  private auth = inject(AuthService);
  private user = toSignal(this.auth.user$);

  rating = input.required<number>();
  productId = input.required<number>();
  ratingArr = computed(() => {
    const stars: string[] = [];
    const fullStars = Math.floor(this.rating());
    const hasHalfStar = this.rating() % 1 !== 0;
    for (let i = 0; i < fullStars; i++) stars.push('star');
    if (hasHalfStar) stars.push('star_half');

    while (stars.length < 5) {
      stars.push('star_outline');
    }
    return stars;
  });

  rateItem(rating: number) {
    if (!this.user()?.email) return;
    // Convert This To ActionDataSource !!
    console.log(rating);
    console.log(this.productId());
  }
}

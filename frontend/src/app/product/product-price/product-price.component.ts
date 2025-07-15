import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, computed, input } from '@angular/core';
@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-product-price',
  imports: [CommonModule],
  templateUrl: './product-price.component.html',
})
export class ProductPriceComponent {
  grossPrice = input.required<number>();
  discount = input<number>(0);
  discountedPrice = computed(() => this.grossPrice() - this.grossPrice() * this.discount());
}

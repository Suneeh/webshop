import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { RouterLink } from '@angular/router';
import { GetProductListDto } from '../../services/api.service.ts/dtos/products/get-product-list-dto';
import { ProductPriceComponent } from '../product-price/product-price.component';
import { ProductRatingComponent } from '../product-rating/product-rating.component';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-product-list',
  imports: [CommonModule, MatCardModule, MatButton, RouterLink, ProductRatingComponent, ProductPriceComponent],
  templateUrl: './product-list.component.html',
})
export class ProductListComponent {
  products = input.required<GetProductListDto[]>();
}

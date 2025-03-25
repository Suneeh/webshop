import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { GetProductListDto } from '../../services/api.service.ts/dtos/products/get-product-list-dto';
import { MatCardModule } from '@angular/material/card';
import { ProductRatingComponent } from '../product-rating/product-rating.component';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-product-list',
  imports: [CommonModule, MatCardModule, MatButton, RouterLink, ProductRatingComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss',
})
export class ProductListComponent {
  products = input.required<GetProductListDto[]>();
}

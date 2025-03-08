import { GetProductListDto } from '../products/get-product-list-dto';

export interface GetCategoryDetailDto {
  id: number;
  name: string;
  description?: string | null;
  products: GetProductListDto[];
}

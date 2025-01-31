import { GetProductDto } from "../products/get-product-dto";

export interface GetCategoryDto {
    id: number;
    name: string;
    description: string;
    products: GetProductDto[];
}
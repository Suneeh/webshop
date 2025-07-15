export interface GetProductDetailDto {
  id: number;
  name: string;
  description: string;
  color: string;
  creationDate: Date;
  changedDate: Date;
  netPrice: number;
  taxRate: number;
  rating: number;
  discount: number;
}

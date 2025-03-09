export interface GetProductDto {
  id: number;
  name: string;
  description: string;
  color: string;
  creationDate: Date;
  changedDate: Date;
  netPrice: number;
  taxRate: number;
}

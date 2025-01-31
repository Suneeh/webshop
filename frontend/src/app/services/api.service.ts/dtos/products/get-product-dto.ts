export interface GetProductDto {
    id: number;
    name: string;
    description: string;
    creationDate: Date;
    changedDate: Date;
    netPrice: number;
    taxRate: number;
}
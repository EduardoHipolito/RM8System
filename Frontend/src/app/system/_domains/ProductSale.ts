import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase';
import { Product } from './Product';

@Injectable()
export class ProductSale extends _EntityBase {
     public IdProduct: number;
     public FKProduct: Product;
     public Quantity: number;
}

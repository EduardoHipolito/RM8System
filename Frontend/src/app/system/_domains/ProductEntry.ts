import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase'
import { Product } from './Product'
import { Entry } from './Entry';

@Injectable()
export class ProductEntry extends _EntityBase {
    public UnitPrice: number;
    public ICMS: number;
    public IPI: number;
    public Quantity: number;
    public IdProduct: number;
    public FKProduct: Product;
    public IdEntry: number;
    public FKEntry: Entry;

}

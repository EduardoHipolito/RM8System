import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase'
import { Supplier } from './Supplier';
import { ProductEntry } from './ProductEntry';

@Injectable()
export class Entry extends _EntityBase {
    public IdSupplier: number;
    public FKSupplier: Supplier;
    public TotalPrice: number;
    public Discount: number;
    public Shipping: number;
    public ProductEntries: Array<ProductEntry>;

}

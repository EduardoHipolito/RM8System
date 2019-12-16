import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase';
import { ProductSale } from './ProductSale';
import { Payment } from './Payment';
import { Customer } from './Customer';

@Injectable()
export class Sale extends _EntityBase {
     public IdCustomer: number;
     public FKCustomer: Customer;
     public Discount: number;
     public Shipping: number;
     public Products: Array<ProductSale>;     
     public Payments: Array<Payment>;     
}

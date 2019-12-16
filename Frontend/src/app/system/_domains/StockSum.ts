import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase';

@Injectable()
export class StockSum extends _EntityBase
{
     public IdProduct: number;
     public ProductName: string;
     public Brand: string;
     public Picture: string;
     public InternalNumber: string;
     public BarCode: string;
     public Quantity: number;

}

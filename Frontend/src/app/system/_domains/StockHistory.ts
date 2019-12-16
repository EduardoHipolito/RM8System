import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase';
import { StockType } from './enums/StockType';
import { StockHitType } from './enums/StockHitType';

@Injectable()
export class StockHistory extends _EntityBase {

     public ProductName: string;
     public StockType: StockType;
     public StockHitType: StockHitType;
     public Quantity: number;
     public SupplierName: string;
     public CustomerName: string;
     public CreateDate: Date;
     public IdProduct: number;
}

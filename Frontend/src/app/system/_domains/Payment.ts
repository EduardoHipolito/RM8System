import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase';
import { Customer } from './Customer';
import { FormOfPayment } from './FormOfPayment';
import { Sale } from './Sale';

@Injectable()
export class Payment extends _EntityBase {
     public IdFormOfPayment: number;
     public FKFormOfPayment: FormOfPayment;
     public IdSale: number;
     public FKSale: Sale;
     public NSU: string;
     public Value: number;
     public NumberOfInstallments: number;
     public FormOfPaymentRate: number;
}

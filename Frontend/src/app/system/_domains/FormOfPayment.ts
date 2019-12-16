import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase';

@Injectable()
export class FormOfPayment extends _EntityBase
{
     public Name: string;
     public MinNumberOfInstallments: number;
     public MaxNumberOfInstallments: number;
     public MinimumValue: number;
     public Rate: number;
     public MoreInformation: string;

}

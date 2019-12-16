import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase';
import { LegalPerson } from './LegalPerson';
import { SupplierType } from './enums/SupplierType';

@Injectable()
export class Supplier extends _EntityBase {
     public IdLegalPerson: number;
     public FKLegalPerson: LegalPerson;
     public MoreInformation: string;
     public SupplierType: SupplierType;
}

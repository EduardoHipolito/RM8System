import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase';
import { SupplierType } from './enums/SupplierType';
import { PhysicalPerson } from './PhysicalPerson';

@Injectable()
export class Customer extends _EntityBase {
     public IdPhysicalPerson: number;
     public FKPhysicalPerson: PhysicalPerson;
     public MoreInformation: string;
     public Limit: number;
}

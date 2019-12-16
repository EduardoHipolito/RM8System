import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase'
import { LegalPerson } from './LegalPerson'

@Injectable()
export class Company extends _EntityBase {
    public Type: number;
    public PaymentDay: number;
    public ReducedName: string;
    public IdPerson: number;
    public IdMaster: number;

    public FKPessoa: LegalPerson;
    public FKMaster: Company;
}

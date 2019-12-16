import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase'
import { Person } from './Person'
import { Aplication } from './Aplication';
import { User } from './User';
import { Company } from './Company';

@Injectable()
export class UserAplicationCompany extends _EntityBase {
    public IdAplication: number;
    public FKAplication: Aplication;
    public IdUser: number;
    public FKUser: User;
    public IdCompany: number;
    public FKCompany: Company;
}

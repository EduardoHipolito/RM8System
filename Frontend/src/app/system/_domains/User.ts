import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase'
import { Person } from './Person'
import { ProfileType } from './enums/ProfileType';

@Injectable()
export class User extends _EntityBase {
    public Login: string;
    public Password: string;
    public ProfileType: ProfileType;
    public TokenAlteracaoDeSenha: string;
    public IdPerson: number;
    public FKPerson: Person;

}

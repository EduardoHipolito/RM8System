import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { _EntityBase } from './_EntityBase';
import { Person } from './Person';
import { Country } from './Country';
import { State } from './State';
import { City } from './City';
import { AddressType } from './enums/AddressType';
import { PublicAreaType } from './enums/PublicAreaType';

@Injectable()
export class Address extends _EntityBase {

    public IdPerson: number;
    public FKPerson: Person;
    public Type: AddressType;
    public PublicAreaType: PublicAreaType;
    public PublicArea: string;
    public Complement: string;
    public Number: number;
    public Neighborhood: string;
    public PostalCode: number;
    public FKCountry: Country;
    public IdCountry: number;
    public FKState: State;
    public IdState: number;
    public FKCity: City;
    public IdCity: number;

    constructor() {
        super();
        this.FKPerson = new Person();
    }
}

import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { _EntityBase } from './_EntityBase'
import { Person } from './Person';
import { Country } from './Country';
import { PhoneType } from './enums/PhoneType';

@Injectable()
export class Phone extends _EntityBase {
  public Type: PhoneType;
  public IdCountry: number;
  public FKCountry: Country;
  public AreaCode: number;
  public Number: number;
  public IdPerson: number;
  public FKPerson: Person;


  constructor() {
    super();
    this.FKPerson = new Person();
  }
}

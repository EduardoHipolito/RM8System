import { Injectable } from '@angular/core';
import {_EntityBase} from './_EntityBase'
import { Country } from './Country';
import { PersonType } from './enums/PersonType';

@Injectable()
export class DocumentType extends _EntityBase
{
     public Name: string;
     public PersonType: PersonType;
     public IdCountry: number;
     public FKCountry: Country;

}

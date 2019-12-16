import { Injectable } from '@angular/core';

import {_EntityBase} from './_EntityBase'
import {Country} from './Country';

@Injectable()
export class State extends _EntityBase
{
    public Name: string;
    public Code: string;
    public FKCountry: Country;
    public IdCountry: number;
    constructor()
    {
        super();
        this.FKCountry = new Country();
    }
}

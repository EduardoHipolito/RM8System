import { Injectable } from '@angular/core';

import {_EntityBase} from './_EntityBase';
import {Country} from './Country';
import {State} from './State';

@Injectable()
export class City extends _EntityBase
{
    public Name: string;
    public FKCountry: Country;
    public FKState: State;
    public IdCountry: number;
    public IdState: number;
    constructor()
    {
        super();
        this.FKCountry = new Country();
        this.FKState = new State();
    }
}

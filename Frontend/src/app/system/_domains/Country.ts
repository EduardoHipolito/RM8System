import { Injectable } from '@angular/core';

import {_EntityBase} from './_EntityBase'

@Injectable()
export class Country extends _EntityBase
{
    public Name: string;
    public Code: string;
    public PhoneCode: number;
    constructor()
    {
        super();
    }
}

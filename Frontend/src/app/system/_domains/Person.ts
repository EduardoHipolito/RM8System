import { Injectable } from '@angular/core';

import { Document } from './Document'
import { Address } from './Address'
import { Phone } from './Phone'
import { _EntityBase } from './_EntityBase'

@Injectable()
export class Person extends _EntityBase {
    public Name: string;
    public Email: string;
    public HomePage: string;
    public Documents: Array<Document>;
    public Addresses: Array<Address>;
    public Phones: Array<Phone>;

    constructor() {
        super();
    }
}


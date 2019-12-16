import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { Person } from './Person'
import { Cnae } from './Cnae';

@Injectable()
export class LegalPerson extends Person {
    public FantasyName: string;
    public CorporateName: string;
    public IdCnae: number;
    public FKCnae: Cnae;

}

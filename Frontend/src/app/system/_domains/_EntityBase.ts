import { Injectable } from '@angular/core';
import { EntityType } from './enums/EntityType';

@Injectable()
export class _EntityBase
{
     public Id: number;
     public CreateDate: Date;
     public ModifiedDate: Date;
     public Status: EntityType;
     
    constructor()
    {

    }
}

import { Injectable } from '@angular/core';
import {_EntityBase} from './_EntityBase'
import { Aplication } from './Aplication';

@Injectable()
export class Module extends _EntityBase
{
     public Description: string;
     public Icon:string = "&#9977;";
     public Index: number;
     public Name: string;
     public Aplications: Aplication[];
     public IsSelected:boolean = true;
     public ModuleCode: string;
}

import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase';

@Injectable()
export class Category extends _EntityBase
{
     public Name: string;
     public Description: string;

}

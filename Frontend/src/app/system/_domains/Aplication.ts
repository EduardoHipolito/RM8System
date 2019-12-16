import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase'
import { Module } from './Module'

@Injectable()
export class Aplication extends _EntityBase {
    public IdModule: number;
    public FKModule: Module;
    public Link: string;
    public Description: string;
    public Index: number;
    public Name: string;
    public AplicationCode: string;
    public ShowMenu: boolean;
}

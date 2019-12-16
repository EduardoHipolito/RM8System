import { Injectable } from '@angular/core';

@Injectable()

export class KeyNumberValueString {

    public Key: number;
    public Value: string;

    constructor(key: number, value: string) {
        this.Key = key;
        this.Value = value
    }
}
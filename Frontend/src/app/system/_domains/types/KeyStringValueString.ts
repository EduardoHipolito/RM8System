import { Injectable } from '@angular/core';

@Injectable()

export class KeystringValueString {

    public Key: string;
    public Value: string;

    constructor(key: string, value: string) {
        this.Key = key;
        this.Value = value
    }
}
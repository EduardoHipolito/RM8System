import { Injectable } from '@angular/core';

@Injectable()

export class KeyNumberValueStringObsString {

    public Key: number;
    public Value: string;
    public Obs: string;

    constructor(key: number, value: string, obs: string) {
        this.Key = key;
        this.Value = value;
        this.Obs = obs;
    }
}
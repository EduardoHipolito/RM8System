import { Injectable } from '@angular/core';

@Injectable()
export class AuthProps {
    public UserId: number;
    public access_token: string;
    public expiresIn: number;
    public refresh_token: string;
    public requertAt: Date;
    public tokeyType: string;
    public aplication_codes: string[];
    constructor() { }
}

import { Http } from "@angular/http";
import { AuthCookie } from "../auth/auth.cookie";
import { Router } from "@angular/router";
import { urlBase } from "../helppers/configs";
import { Injectable } from "@angular/core";
import { RequestBaseParameter } from "../models/RequestBase";

@Injectable()
export class DropDownService {

    constructor(private http: Http) {

    }

    public Get(url: string) {
        return this.http.get(url)
            .toPromise();
    }

    public Post(url: string,parameters: any) {
        return this.http.post(url, parameters).toPromise();
    }
}
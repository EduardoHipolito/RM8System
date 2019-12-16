import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { urlBase } from '../../_framework/helppers/configs';
import { RequestBaseParameter } from '../../_framework/models/RequestBase';

@Injectable()
export class AddressService {


    constructor(private http: Http) {
    }

    getFromPostalCode(postal_cod: number) {
        var request = new RequestBaseParameter<string>();
        request.Parameter = postal_cod.toString();
        return this.http.post(urlBase + "/address/GetByCEP", request).toPromise();
    }


}


import { Injectable } from '@angular/core';

import { urlBase } from '../../_framework/helppers/configs';
import { RequestBaseParameter } from '../../_framework/models/RequestBase';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AddressService {


    constructor(private http: HttpClient) {
    }

    getFromPostalCode(postal_cod: number) {
        var request = new RequestBaseParameter<string>();
        request.Parameter = postal_cod.toString();
        return this.http.post(urlBase + "/address/GetByCEP", request).toPromise();
    }


}


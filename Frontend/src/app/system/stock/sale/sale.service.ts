import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { urlStock } from '../../_framework/helppers/configs';
import { RequestBaseParameter } from '../../_framework/models/RequestBase';

@Injectable()
export class SaleService {

    constructor(private http: Http) {
    }

    Save(form: any) {
        var request = new RequestBaseParameter<any>();
        request.Parameter = form;
        const requestOptions = { headers: new Headers({ 'OperationType': 'WRITE' }) };
        return this.http.post(urlStock + "/Sale/Add", request, requestOptions).toPromise();
      }
}


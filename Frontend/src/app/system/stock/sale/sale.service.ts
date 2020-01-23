import { Injectable } from '@angular/core';

import { urlStock } from '../../_framework/helppers/configs';
import { RequestBaseParameter } from '../../_framework/models/RequestBase';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class SaleService {

    constructor(private http: HttpClient) {
    }

    Save(form: any) {
        var request = new RequestBaseParameter<any>();
        request.Parameter = form;
        const requestOptions = { headers: new HttpHeaders({ 'OperationType': 'WRITE' }) };
        return this.http.post(urlStock + "/Sale/Add", request, requestOptions).toPromise();
      }
}


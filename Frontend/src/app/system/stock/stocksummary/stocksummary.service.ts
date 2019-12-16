
import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions, RequestOptionsArgs } from '@angular/http';

import 'rxjs/Rx';
import { _EntityBase } from '../../_domains/_EntityBase';
import { EntityType } from '../../_domains/enums/EntityType';
import { urlStock } from '../../_framework/helppers/configs';
import { RequestBaseParameter } from '../../_framework/models/RequestBase';
import { RequestById } from '../../_framework/models/RequestById';

@Injectable()
export class StockSummaryService {

  constructor(private http: Http, ) {
  }

  GetSummary() {
    const requestOptions = { headers: new Headers({ 'OperationType': 'READ' }) };
    return this.http.get(urlStock + "/stock/GetSummary", requestOptions).toPromise();
  }

  GetHistoryByIdProduct(id: any) {
    var request = new RequestById();
    request.Id = id;
    const requestOptions = { headers: new Headers({ 'OperationType': 'READ' }) };
    return this.http.post(urlStock + "/stock/GetHistoryByIdProduct", request, requestOptions).toPromise();
  }

}

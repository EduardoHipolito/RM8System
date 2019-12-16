import { Injectable, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

import { Http, Response, Headers, RequestOptions } from '@angular/http';
import 'rxjs/Rx';
import { urlBase, urlStock } from '../../_framework/helppers/configs';

@Injectable()
export class HomeService {

  constructor(private http: Http) {

  }

  public GetDailySalesAmout() {
    return this.http.get(urlStock + "/Sale/GetDailySalesAmout")
      .toPromise();
  }

  public GetMonthSalesAmout() {
    return this.http.get(urlStock + "/Sale/GetMonthSalesAmout")
      .toPromise();
  }

  public GetDailyProfit() {
    return this.http.get(urlStock + "/Sale/GetDailyProfit")
      .toPromise();
  }

  public GetMonthprofit() {
    return this.http.get(urlStock + "/Sale/GetMonthProfit")
      .toPromise();
  }

  public GetDailyEntries() {
    return this.http.get(urlStock + "/Entry/GetDailyEntries")
      .toPromise();
  }

  public GetMonthEntries() {
    return this.http.get(urlStock + "/Entry/GetMonthEntries")
      .toPromise();
  }
}

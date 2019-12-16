import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions, RequestOptionsArgs } from '@angular/http';

import 'rxjs/Rx';
import { RequestBaseParameter } from '../models/RequestBase';
import { RequestById } from '../models/RequestById';
import { _EntityBase } from '../../_domains/_EntityBase';
import { EntityType } from '../../_domains/enums/EntityType';
import { GridRequestSettings } from '../grid/GridSetings';
import { RequestGrid, RequestGridParameter } from '../models/RequestGrid';

@Injectable()
export class CrudplugService {

  constructor(private http: Http, ) {
  }
  public UrlBase: string;

  SetUrlBase(Url: string) {
    this.UrlBase = Url;
  }

  GetAllGrid(gridSettings: GridRequestSettings) {
    var request = new RequestGrid();
    request.Settings = gridSettings;
    const requestOptions = { headers: new Headers({ 'OperationType': 'READ' }) };
    return this.http.post(this.UrlBase + "/getallgrid", request, requestOptions).toPromise();
  }

  FindAll(gridSettings: GridRequestSettings, form: any) {
    var request = new RequestGridParameter<any>();
    request.Parameter = form;
    request.Settings = gridSettings;
    const requestOptions = { headers: new Headers({ 'OperationType': 'READ' }) };
    return this.http.post(this.UrlBase + "/getallgrid", request, requestOptions).toPromise();
  }

  Add(form: any) {
    var request = new RequestBaseParameter<any>();
    request.Parameter = form;
    const requestOptions = { headers: new Headers({ 'OperationType': 'WRITE' }) };
    return this.http.post(this.UrlBase + "/Add", request, requestOptions).toPromise();
  }

  Update(form: any) {
    var request = new RequestBaseParameter<any>();
    request.Parameter = form;
    const requestOptions = { headers: new Headers({ 'OperationType': 'WRITE' }) };
    return this.http.put(this.UrlBase + "/Update", request, requestOptions).toPromise();
  }

  Delete(form: any) {
    var request = new RequestById();
    request.Id = form.Id;
    const requestOptions = { headers: new Headers({ 'OperationType': 'WRITE' }) };
    return this.http.post(this.UrlBase + "/Delete", request, requestOptions).toPromise();
  }
}

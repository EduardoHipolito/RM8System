import { Injectable, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
 
import 'rxjs/Rx';
import { AuthCookie } from '../../../auth/auth.cookie';
import { urlBase } from '../../../helppers/configs';
import { RequestById } from '../../../models/RequestById';
import { HttpClient } from '@angular/common/http';


@Injectable()
export class SelectCompanyService {


  protected _currentMenuItem = {};

  constructor(private http: HttpClient, private _coockie: AuthCookie,
    private router: Router) {

  }

  public updateCompanies() {
    
    return this.http.get(urlBase + "/Company/GetCompanyByUser")
        .toPromise();
  }

  public setLojaPadrao(Id: string) {
    if (Id != null) {
      this._coockie.SetIntoLocalStorage('IdCompany', Id);
      
    }
  }

}

import { Injectable } from '@angular/core';
import { Request, XHRBackend, BrowserXhr, ResponseOptions, XSRFStrategy, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { NotificationsService } from '../../_framework/notification/notifications.service';
import { AuthCookie } from './../auth/auth.cookie'
import { ResponseResult } from '../models/ResponseResult';
import { ResponseState } from '../models/enum/ResponseState';
import { PageLoaderService } from '../pageLoader/page-loader.service';
import { AuthService } from '../auth/auth.service';
// import { PageLoaderService } from '../../_framework/pageLoader/page-loader.service'

@Injectable()
export class ExtendedXHRBackend extends XHRBackend {

  constructor(private notificationsService: NotificationsService,
    private pageLoaderService: PageLoaderService,
    private router: Router,
    private authCookie: AuthCookie,
    browserXhr: BrowserXhr,
    baseResponseOptions: ResponseOptions,
    xsrfStrategy: XSRFStrategy) {
    super(browserXhr, baseResponseOptions, xsrfStrategy);
  }

  createConnection(request: Request) {
    this.pageLoaderService.LoadOn();
    let access_token = this.authCookie.GetIntoLocalStorage('access_token');
    let refresh_token = this.authCookie.GetIntoLocalStorage('refresh_token');
    let IdCompany = this.authCookie.GetIntoLocalStorage('IdCompany');
    let UserId = this.authCookie.GetIntoLocalStorage('UserId');
    request.headers.set('Accept', 'application/json');
    request.headers.set('Access-Control-Allow-Origin', '*');
    request.headers.set('Content-Type', 'application/json; charset=utf-8');
    request.headers.set('Authorization', `Bearer ${access_token}`);
    request.headers.set('refresh_token', `${refresh_token}`);
    request.headers.set('IdCompany', `${IdCompany}`); 
    request.headers.set('UserId', `${UserId}`);
    let xhrConnection = super.createConnection(request);

    xhrConnection.response = xhrConnection.response
      .do((res: any) => {
        let headers = res.headers;
        let Newaccess_token: string = headers.get('access_token');
        let Newrefresh_token: string = headers.get('refresh_token');
        if (Newaccess_token != null) {
          this.authCookie.SetIntoLocalStorage("access_token", Newaccess_token);
        }
        if (Newrefresh_token != null) {
          this.authCookie.SetIntoLocalStorage("refresh_token", Newrefresh_token);
        }
        this.pageLoaderService.LoadOff();

        var result = <ResponseResult>res.json();
        if(result != null){
          if(result.State == ResponseState.Failed){
            this.notificationsService.error(result.Msg);
          }
          if(result.State == ResponseState.NotAuth){
            this.notificationsService.alert(result.Msg);
          }
        }

      })
      .catch((res: Response) => {
        switch (res.status) {
          case 401:
            {
              this.notificationsService.alert('acesso não autorizado');
              this.authCookie.clearStorage();
              this.router.navigate(['/login']);
              break;
            }

          case 403:
            {
              this.notificationsService.alert('acesso não autorizado');
              this.authCookie.clearStorage();
              this.router.navigate(['/login']);
              break;
            }

          case 500:
            {
              console.log('Ocorreu uma falha:');
              if (res.json().Ex !== null) {
                console.log(res.json().Ex);
              }
              this.notificationsService.error(res.json());
              break;
            }

          case 200:
            {
              break;
            }
          case 0:
            {
              this.notificationsService.alert('acesso não autorizado');
              this.authCookie.clearStorage();
              this.router.navigate(['/login']);
              break;
            }
          default:
            {
              break;
            }
        }
        this.pageLoaderService.LoadOff();
        return Observable.throw(res);
      });

    return xhrConnection;
  }
}
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { AuthCookie } from './../auth/auth.cookie'
import { AuthService } from '../auth/auth.service';
import { NotificationsService } from '../notification/notifications.service';
import { PageLoaderService } from '../pageLoader/page-loader.service';
import { ResponseResult } from '../models/ResponseResult';
import { ResponseState } from '../models/enum/ResponseState';
import { Router } from '@angular/router';
// import { PageLoaderService } from '../../_framework/pageLoader/page-loader.service'

@Injectable()
export class ExtendedHttpInterceptor implements HttpInterceptor {

  constructor(private notificationsService: NotificationsService,
    private pageLoaderService: PageLoaderService,
    private router: Router,
    private authCookie: AuthCookie) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.pageLoaderService.LoadOn();
    let access_token = this.authCookie.GetIntoLocalStorage('access_token');
    // let refresh_token = this.authCookie.GetIntoLocalStorage('refresh_token');
    let IdCompany = this.authCookie.GetIntoLocalStorage('IdCompany');
    let UserId = this.authCookie.GetIntoLocalStorage('UserId');

    const dupReq = req.clone({
      headers: req.headers.set('Accept', 'application/json')
        .set('Access-Control-Allow-Origin', '*')
        // .set("Access-Control-Allow-Methods", "DELETE, PUT, POST, GET, OPTIONS")
        // .set("Access-Control-Allow-Headers", "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With")
        .set('Content-Type', 'application/json; charset=utf-8')
        .set('Authorization', `Bearer ${access_token}`)
        // .set('refresh_token', `${refresh_token}`)
        .set('IdCompany', `${IdCompany}`)
        .set('UserId', `${UserId}`),
    });

    return next.handle(dupReq).pipe(
      map((event: HttpEvent<any>) => {


        if (event instanceof HttpResponse) {

          // let headers = <HttpHeaders>event.headers;
          // let Newaccess_token: string = headers.get('access_token');
          // let Newrefresh_token: string = headers.get('refresh_token');
          // if (Newaccess_token != null) {
          //   this.authCookie.SetIntoLocalStorage("access_token", Newaccess_token);
          // }
          // if (Newrefresh_token != null) {
          //   this.authCookie.SetIntoLocalStorage("refresh_token", Newrefresh_token);
          // }

          var result = <ResponseResult>event.body;
          if (result != null) {
            if (result.State == ResponseState.Failed) {
              this.notificationsService.error(result.Msg);
            }
            if (result.State == ResponseState.NotAuth) {
              this.notificationsService.alert(result.Msg);
            }
          }
        }
        this.pageLoaderService.LoadOff();
        return event;
      }),
      catchError((error: HttpErrorResponse) => {
        switch (error.status) {
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
              this.notificationsService.error(error.message);
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
        return throwError(error);
      }));
  }
}
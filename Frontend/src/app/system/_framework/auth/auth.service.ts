import { Injectable, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

import { Http, Response, Headers, RequestOptions } from '@angular/http';
import 'rxjs/Rx';

import { AuthCookie } from './auth.cookie';
import { AuthProps } from './auth.Props';

import { UserViewModel } from './user.interface';
import { NotificationsService } from '../notification/notifications.service';
import { Company } from '../../_domains/Company';
import { urlBase, loginStock } from '../helppers/configs';
import { ResponseResult } from '../models/ResponseResult'
import { RequestById } from '../models/RequestById';
import { ResponseState } from '../models/enum/ResponseState';


@Injectable()
export class AuthService {

    constructor(
        private notificationsService: NotificationsService,
        private authCookie: AuthCookie,
        private http: Http,
        private authProps: AuthProps,
        private router: Router) { }
    private companies: Array<Company>;

    signIn(user: UserViewModel) {

        let headers = new Headers({ 'Accept': 'application/json' });
        headers.append('Content-Type', `application/x-www-form-urlencoded`);
        let options = new RequestOptions({ headers: headers });
        let context = this;
        this.http.post(loginStock + "/Token/Login", user, options)
            .toPromise()
            .then(response => {
                var res: ResponseResult = response.json();
                if (res.State == ResponseState.Success) {
                    context.authProps = <AuthProps>res.Data;
                    if (context.authProps.access_token != null) {
                        context.authCookie.SetIntoLocalStorage("access_token", context.authProps.access_token);
                        context.authCookie.SetIntoLocalStorage("token_type", context.authProps.tokeyType);
                        context.authCookie.SetIntoLocalStorage("expires_in", context.authProps.expiresIn);
                        context.authCookie.SetIntoLocalStorage("refresh_token", context.authProps.refresh_token);
                        context.authCookie.SetIntoLocalStorage("UserId", context.authProps.UserId);
                        this.getCompanies()
                            .then(response => {
                                var res: ResponseResult = response.json();

                                if (res.State == ResponseState.Success) {

                                    context.companies = <Array<Company>>res.Data;

                                    context.authCookie.SetIntoLocalStorage("IdCompany", context.companies[0].Id);
                                    context.router.navigateByUrl('/system/base/home');
                                }
                            })
                            .catch(error => {
                                context.notificationsService.error(error);
                            })
                    }
                }
            });
    }

    logout() {
        this.authCookie.clearStorage();
        this.router.navigate(['/login']);
    }

    isAuthenticated() {
        if (this.authCookie.GetIntoLocalStorage("UserId") > 0) {
            return true;
        }
        else {
            return false;
        }
    }
    public getCompanies() {

        return this.http.get(urlBase + "/Company/GetCompanyByUser")
            .toPromise();
    }
    public hasPermition(code: string) {
        var permitions = this.authCookie.GetIntoLocalStorage('aplication_codes') as string[];
        return permitions != null && permitions.includes(code);
    }
}

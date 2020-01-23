import { Injectable, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

import 'rxjs/Rx';

import { urlBase } from '../helppers/configs';
import { RequestById } from '../models/RequestById';
import { AuthCookie } from '../auth/auth.cookie';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class SidebarService {


  public isSubMenuVisible: boolean = false;
  public isMenuNotCollapsed: boolean = true;

  protected _currentMenuItem = {};

  constructor(private http: HttpClient, private _coockie: AuthCookie,
    private router: Router) {

  }


  public updateMenus() {
    return this.http.get(urlBase + "/UserAplicationCompany/GetMenu")
      .toPromise();
  }


  public updateRoles() {
    return this.http.get(urlBase + "/UserAplicationCompany/GetRoles")
      .toPromise();
  }

  public getCurrentItem(): any {
    return this._currentMenuItem;
  }

  public selectMenuItem(menuItems: any[]): any[] {
    let items = [];
    menuItems.forEach((item) => {

      if (item.selected) {
        this._currentMenuItem = item;
      }

      if (item.children && item.children.length > 0) {
        item.children = this.selectMenuItem(item.children);
      }
      items.push(item);
    });
    return items;
  }

}

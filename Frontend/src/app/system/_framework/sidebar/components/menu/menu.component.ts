import {Component, ViewEncapsulation, Input, Output, EventEmitter} from '@angular/core';
import {Router, Routes, NavigationEnd} from '@angular/router';

import {Module} from '../../../../_domains/Module';
import { Subscription } from 'rxjs';

@Component({
  selector: 'menu',
  encapsulation: ViewEncapsulation.None,
  styleUrls : ['./menu.css'],
  templateUrl: './menu.html'
})
export class Menu {

  @Input() sidebarCollapsed: boolean;
  @Input() modules: Module[];
  @Output() ClickEvent = new EventEmitter();
  @Input() height:number;

  protected _menuItemsSub: Subscription;
  protected _onRouteChange:Subscription;

  constructor(private _router:Router) {
  }

  public updateMenu(newMenuItems) {
    this.modules = newMenuItems;
  }

  public ngOnDestroy():void {
    // this._onRouteChange.unsubscribe();
    // this._menuItemsSub.unsubscribe();
  }

}

import { Component, ViewEncapsulation, Input, Output, EventEmitter } from '@angular/core';
import { Router, Routes, NavigationEnd } from '@angular/router';


import { Aplication } from '../../../../_domains/Aplication';
import { Subscription } from 'rxjs';

@Component({
  selector: 'subMenu',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./submenu.css'],
  templateUrl: './submenu.html'
})
export class SubMenu {

  @Output() CloseEvent = new EventEmitter();
  @Input() aplications: Aplication[];
  @Input() ShowElement: boolean;
  @Input() height: number;

  protected _menuItemsSub: Subscription;
  protected _onRouteChange: Subscription;

  constructor(private _router: Router) {

  }

  public updateMenu(newMenuItems) {
    this.aplications = newMenuItems;
  }

  public ngOnInit(): void {
  }

  public ngOnDestroy(): void {
    // this._onRouteChange.unsubscribe();
    // this._menuItemsSub.unsubscribe();
  }

}

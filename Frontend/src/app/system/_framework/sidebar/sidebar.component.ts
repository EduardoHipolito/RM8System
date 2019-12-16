import { Component, AfterViewInit, ElementRef, HostListener, Input, Output, ViewEncapsulation } from '@angular/core';

import { SidebarService } from './sidebar.service';
import { Module } from '../../_domains/Module';
import { Aplication } from '../../_domains/Aplication';
import { AuthCookie } from '../auth/auth.cookie';

@Component({
  selector: 'sidebar',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./sidebar.component.css'],
  templateUrl: './sidebar.component.html'
})

export class Sidebar {
  public modules: Module[];
  public aplications: Aplication[];

  public height: number = window.innerHeight;

  get _isMenuNotCollapsed(){
    return this._service.isMenuNotCollapsed;
  }
  get _isSubMenuVisible(){
    return this._service.isSubMenuVisible;
  }
  
  constructor(private _service: SidebarService,
    private authCookie: AuthCookie,
     private _elementRef: ElementRef) {
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.height = window.innerHeight;
  }

  public ngOnInit(): void {
    this._service.updateMenus()
      .then(response => {
        var res = response.json();
        this.modules = res.Data;
        this.aplications = this.modules[0].Aplications;
        this._service.isMenuNotCollapsed = true;
      });

      this._service.updateRoles()
        .then(response => {
          var res = response.json();
          this.authCookie.SetIntoLocalStorage("aplication_codes", res.Data);
        });
  }

  moduleClick(val) {

    if (val.value.IsSelected == true) {
      this.modules.forEach(element => {
        element.IsSelected = false;
      });
      this.aplications = null;
      this._service.isSubMenuVisible = false;
      let MainContend: any = document.querySelector('grid');
      if (MainContend != null) {
        MainContend.style.width = "95%";
      }
    }
    else {
      this.modules.forEach(element => {
        element.IsSelected = false;
      });
      val.value.IsSelected = true;
      this.aplications = this.modules.filter(x => x.Id === val.value.Id)[0].Aplications;
      this._service.isSubMenuVisible = true;
      let MainContend: any = document.querySelector('grid');
      if (MainContend != null) {
        MainContend.style.width = "85%";
      }
    }
  }

  ColapseClickEvent(event) {
    this._service.isMenuNotCollapsed = !this._service.isMenuNotCollapsed;
  }

  CloseClick() {
    this.modules.forEach(element => {
      element.IsSelected = false;
    });
    this.aplications = null;
    this._service.isSubMenuVisible = false;
    let MainContend: any = document.querySelector('grid');
    if (MainContend != null) {
      let wid = (Number(MainContend.style.width.replace("%", "")) + 10).toString() + "%";
      MainContend.style.width = wid;
    }
  }
}
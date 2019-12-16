import { Component, OnInit } from '@angular/core';
import { SidebarService } from './_framework/sidebar/sidebar.service';


@Component({
  selector: 'app-system',
  templateUrl: './system.component.html'
})
export class SystemComponent implements OnInit {

  constructor(private sidebarService: SidebarService) {
  }

  ngOnInit() {
  }
  get _mainContendHeight(): number {

    if (this.sidebarService.isMenuNotCollapsed && this.sidebarService.isSubMenuVisible) {
      return 75;
    } else
      if (!this.sidebarService.isMenuNotCollapsed && !this.sidebarService.isSubMenuVisible) {
        return 90;
      } else
        if (this.sidebarService.isMenuNotCollapsed && !this.sidebarService.isSubMenuVisible) {
          return 85;
        } else
          if (!this.sidebarService.isMenuNotCollapsed && this.sidebarService.isSubMenuVisible) {
            return 80;
          }
  }
}







import {Component, ViewEncapsulation, Input, Output, EventEmitter} from '@angular/core';
import { SidebarService } from '../../sidebar.service';

@Component({
  selector: 'SubMenuItem',
  encapsulation: ViewEncapsulation.None,
  styleUrls : ['./subMenuItem.css'],
  templateUrl: './subMenuItem.html'
})
export class SubMenuItem {

  @Input() subMenuItem:any;
  @Input() child:boolean = false;
  
  constructor(private _service: SidebarService) {
  }

  onClick(){
    this._service.isSubMenuVisible = false;
  }
}

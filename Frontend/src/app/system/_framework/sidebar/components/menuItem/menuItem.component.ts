import {Component, ViewEncapsulation, Input, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'MenuItem',
  encapsulation: ViewEncapsulation.None,
  styleUrls : ['./menuItem.css'],
  templateUrl: './menuItem.html'
})
export class MenuItem {

  @Input() isColapsed:boolean;
  @Input() menuItem:any;
  @Input() child:boolean = false;

}

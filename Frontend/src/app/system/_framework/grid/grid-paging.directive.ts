import {Directive, EventEmitter, Input, Output, HostListener} from '@angular/core';

@Directive({selector: '[GridPaging]'})
export class GridPagingDirective {
  @Input() public GridPaging:boolean = true;
  @Output() public tableChanged:EventEmitter<any> = new EventEmitter();

  @Input()
  public get config():any {
    return this.GridPaging;
  }

  public set config(value:any) {
    this.GridPaging = value;
  }

  @HostListener('pageChanged', ['$event'])
  public onChangePage(event:any):void {
    // Object.assign(this.config, event);
    if (this.GridPaging) {
      this.tableChanged.emit({paging: event});
    }
  }
}

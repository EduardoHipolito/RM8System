import { Directive, EventEmitter, Input, Output, HostListener } from '@angular/core';
import { GridSetings, GridColumnSetings } from './GridSetings';

@Directive({ selector: '[GridSorting]' })
export class GridSortingDirective {
  @Input() public GridSorting: GridColumnSetings;
  @Output() public sortChanged: EventEmitter<any> = new EventEmitter();

  @Input()
  public get config(): any {
    return this.GridSorting;
  }

  public set config(value: any) {
    this.GridSorting = value;
  }

  @HostListener('click', ['$event', '$target'])
  public onToggleSort(event: any): void {
    if (event) {
      event.preventDefault();
    }

    if (this.GridSorting && this.GridSorting.sort && this.GridSorting.sort.able !== false) {
      switch (this.GridSorting.sort.direction) {
        case 'asc':
          this.GridSorting.sort.direction = 'desc';
          break;
        case 'desc':
          this.GridSorting.sort.direction = '';
          break;
        default:
          this.GridSorting.sort.direction = 'asc';
          break;
      }

      this.sortChanged.emit(this.GridSorting);
    }
  }
}

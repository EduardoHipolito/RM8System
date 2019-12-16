import { Directive, EventEmitter, ElementRef, Renderer, HostListener, Input, Output } from '@angular/core';
import { GridFilterSetings } from './GridSetings';

function setProperty(renderer: Renderer, elementRef: ElementRef, propName: string, propValue: any): void {
  renderer.setElementProperty(elementRef, propName, propValue);
}
const ESCAPE_KEYCODE = 13;
@Directive({ selector: '[GridFiltering]' })
export class GridFilteringDirective {
  @Input() public GridFiltering: GridFilterSetings = new GridFilterSetings();

  @Output() public tableChanged: EventEmitter<any> = new EventEmitter();

  @Input()
  public get config(): any {
    return this.GridFiltering;
  }

  public set config(value: any) {
    this.GridFiltering = value;
  }

  private element: ElementRef;
  private renderer: Renderer;
  // @HostListener('document:keydown.escape', ['$event']) onKeydownHandler(event: KeyboardEvent) {
  //   if (event.keyCode === ESCAPE_KEYCODE) {

  //   }
  // }

  @HostListener('keydown', ['$event'])
  public onChangeFilter(event: any): void {
    this.GridFiltering.filterString = event.target.value;
    if (event.keyCode === ESCAPE_KEYCODE) {
      this.tableChanged.emit({ filtering: this.GridFiltering });
    }
  }

  public constructor(element: ElementRef, renderer: Renderer) {
    this.element = element;
    this.renderer = renderer;
    setProperty(this.renderer, this.element, 'value', this.GridFiltering.filterString);
  }
}

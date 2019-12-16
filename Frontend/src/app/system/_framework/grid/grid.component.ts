import { Component, ViewEncapsulation, AfterViewInit, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { GridSortingDirective } from './grid-sorting.directive';
import { GridSetings, GridColumnSetings } from './GridSetings';
import { baseComponent } from '../helppers/base.component';
import { CrudPlugbuttonsType } from '../crudplug/CrudPlugbuttonsType';
import { GridPaginationComponent } from './gridpagination/gridpagination.component';

@Component({
  selector: 'Grid',
  templateUrl: './grid.html',
  styleUrls: ['./grid.css']
})
export class GridComponent implements AfterViewInit {

  ngAfterViewInit(): void {
    this.setings.gridComponentOnLoadCallback = this.OnLoad;
    this.setings.gridContext = this;
  }

  @ViewChild(GridPaginationComponent) _gridPaginationComponent: GridPaginationComponent;

  @Output() click = new EventEmitter<any>();
  @Input() public setings: GridSetings = new GridSetings();

  private _columns: Array<any> = [];

  public set columns(values: Array<any>) {
    values.forEach((value: any) => {
      let column = this._columns.find((col: any) => col.name === value.name);
      if (column) {
        Object.assign(column, value);
      }
      if (!column) {
        this._columns.push(value);
      }
    });
  }

  public get columns(): Array<any> {
    return this.setings.columns != null ? this.setings.columns : new Array<GridColumnSetings>();
  }

  public get configColumns(): any {
    let sortColumns: Array<any> = [];

    this.columns.forEach((column: any) => {
      if (column.sort) {
        sortColumns.push(column);
      }
    });

    return { columns: sortColumns };
  }

  public getData(row: any, propertyName: string): string {
    if (propertyName.indexOf('?') > 0) {
      var question1 = propertyName.split('?');
      var question2 = question1[1].split(':');
      if (row[question1[0]] != null) {
        return question2[0].split('.').reduce((prev: any, curr: string) => prev[curr], row);
      } else {
        return question2[1];
      }
    } else {
      return propertyName.split('.').reduce((prev: any, curr: string) => prev[curr], row);
    }
  }

  onChangeItensPerPage(val) {
    this.setings.RequestSettings.ItemsPerPage = val;
    if (val == 'all') {
      $('pagination-controls').hide();
    }
    else {
      $('pagination-controls').show();
    }
  }

  ItemClick(event, row) {

    this.click.next(row);
    if (this.setings.form != null) {
      this.setings.form.patchValue(row);
    }
    if (this.setings.crudPlugContext) {
      this.setings.crudPlugContext.ChangeFormState('Exist');
      this.setings.crudPlugContext.GridForm()
    }
  }

  public OnColumnFilter(column: GridColumnSetings) {

    if (!column.filter) {
      return;
    }
    var json = '{' + this.setings.columns.filter(f => f.filter != null).map(m => '"' + (m.sortName != null ? m.sortName : m.name) + '":"' + m.filter.filterString + '"') + '}';
    var filterObject = JSON.parse(json);

    // let filteredData: Array<any> = this.setings.data.filter((item: any) =>
    //   String(item[column.name]).toUpperCase().match(String(column.filter.filterString).toUpperCase()));
    // this.setings.rows = filteredData;

    this.setings.RequestSettings.Filter = filterObject;
    this.setings.crudPlugContext.onCrudClick(CrudPlugbuttonsType.Refresh)
  }

  public onColumnOrder(column: any): void {
    this.setings.RequestSettings.ColumOrder = column.name;
    this.setings.RequestSettings.OrderDirection = column.sort.direction;
    this.setings.crudPlugContext.onCrudClick(CrudPlugbuttonsType.Refresh)
    // this._columns.forEach((col: any) => {
    //   if (col.name !== column.name) {
    //     col.sort = '';
    //   }
    // });
  }

  public onPaging(page: any): void {
    this.setings.RequestSettings.CurrentPage = page;
    this.setings.crudPlugContext.onCrudClick(CrudPlugbuttonsType.Refresh)
  }

  openModal(modalId: string) {
    $('#' + modalId).modal('show');
  }

  modalClick(modalId: string, Id: number, modalCallback: (Id: number, context: baseComponent, modalId: string, FinishAndOpenModalCallback: (modalId: string) => void) => void) {
    modalCallback(Id, this.setings.context, modalId, this.openModal);
  }

  OnLoad(gridContext: GridComponent) {
    gridContext._gridPaginationComponent.OnLoad();
  }
}
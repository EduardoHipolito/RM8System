import { Component, OnInit, ViewChild } from '@angular/core';
import { Methods } from './methods';
import { baseComponent } from './base.component';
import { DropDownSetings } from '../dropdown/DropDownSetings';
import { EntityType } from '../../_domains/enums/EntityType';
import { CrudplugComponent } from '../crudplug/crudplug.component';
import { GridComponent } from '../grid/grid.component';

export abstract class baseCrudComponent extends baseComponent implements OnInit {

  constructor() {
    super();
  }
  public config: any;
  public rows: Array<any> = [];
  DdlStatusSetings: DropDownSetings = new DropDownSetings();

  @ViewChild(CrudplugComponent) _crudplugComponent: CrudplugComponent;
  @ViewChild(GridComponent) _GridComponent: GridComponent;

  public abstract InitializeComponent(): void;
  public GridClickCallBack: (value: any) => void;

  ngOnInit(): void {
    let typeList = Methods.EnumToArray(EntityType);
    this.DdlStatusSetings.DataSource = typeList;
    this.InitializeComponent();
  } 
  

  public changeSort(data: any, config: any): any {
    if (!config.sorting) {
      return data;
    }

    let columns = this.config.sorting.columns || [];
    let columnName: string = void 0;
    let sort: string = void 0;

    for (let i = 0; i < columns.length; i++) {
      if (columns[i].sort !== '') {
        columnName = columns[i].name;
        sort = columns[i].sort;
      }
    }

    if (!columnName) {
      return data;
    }
    // simple sorting
    return data.sort((previous: any, current: any) => {
      if (previous[columnName] > current[columnName]) {
        return sort === 'desc' ? -1 : 1;
      } else if (previous[columnName] < current[columnName]) {
        return sort === 'asc' ? -1 : 1;
      }
      return 0;
    });
  }

  public changeFilter(data: any, config: any): any {
    if (!config.filtering) {
      return data;
    }

    let filteredData: Array<any> = data.filter((item: any) =>
      item[config.filtering.columnName].match(this.config.filtering.filterString));

    return filteredData;
  }
  public onChangeTable(value): any {
    if (this.config.filtering) {
      Object.assign(this.config.filtering, this.config.filtering);
    }
    if (this.config.sorting) {
      Object.assign(this.config.sorting, this.config.sorting);
    }
    //data to rows
    let filteredData = this.changeFilter(this.rows, this.config);
    this.rows = this.changeSort(filteredData, this.config);
  }

  GridClick(val) {
    this._crudplugComponent.ChangeFormState("Exist");
    if (this.GridClickCallBack != null) {
      this.GridClickCallBack(val);
    }
  }
}
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { Observable } from 'rxjs';

import { GridComponent } from '../../_framework/grid/grid.component';
import { GridFilteringDirective } from '../../_framework/grid/grid-filtering.directive';
import { GridPagingDirective } from '../../_framework/grid/grid-paging.directive';
import { GridSortingDirective } from '../../_framework/grid/grid-sorting.directive';
import { baseCrudComponent } from '../../_framework/helppers/baseCrud.component';

import { NotificationsService } from '../../_framework/notification/notifications.service';
import { City } from '../../_domains/City';
import { urlBase } from '../../_framework/helppers/configs';
import { GridSetings, GridColumnSetings, GridSortSetings, GridFilterSetings, BooleanType } from '../../_framework/grid/GridSetings';
import { CrudPlugSetings } from '../../_framework/crudplug/CrudPlugSetings';
import { DropDownSetings, MethodEnum } from '../../_framework/dropdown/DropDownSetings';
import { RequestById } from '../../_framework/models/RequestById';
import { DropDownComponent } from '../../_framework/dropdown/dropdown.component';
import { EntityType } from '../../_domains/enums/EntityType';

@Component({
  selector: 'city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent extends baseCrudComponent {
  public url: string = urlBase + "/city";
  public currentItem: City;
  public form: FormGroup;

  public _gridSetings: GridSetings;
  public _crudPlugSetings: CrudPlugSetings;
  DdlCountriesSetings: DropDownSetings = new DropDownSetings();
  DdlStatesSetings: DropDownSetings = new DropDownSetings();
  
  @ViewChild('ddlState') ddlState: DropDownComponent;


  constructor(private _fb: FormBuilder, private notificationsService: NotificationsService) {
    super();
  }

  public initForm() {
    return this._fb.group({
      Id: [null],
      CreateDate: [null],
      ModifiedDate: [null],
      Status: [EntityType.Ativo, Validators.required],
      Name: ['', Validators.required],
      IdCountry: ['', Validators.required],
      IdState: ['', Validators.required],
      PhoneCode: ['', Validators.required],
    });
  }

  InitializeComponent() {
    this.currentItem = new City();
    this.form = this.initForm();

    var columnId = new GridColumnSetings();
    columnId.name = 'Id';
    columnId.title = 'Id';
    columnId.sort = new GridSortSetings();
    columnId.sort.direction = 'asc';
    columnId.sort.current = true;
    columnId.filter = new GridFilterSetings();
    columnId.filter.filterString = '';
    columnId.filter.placeholder = 'Id';
    columnId.type = 'text';

    var columnName = new GridColumnSetings();
    columnName.name = 'Name';
    columnName.title = 'Nome';
    columnName.sort = new GridSortSetings();
    columnName.filter = new GridFilterSetings();
    columnName.filter.filterString = '';
    columnName.filter.placeholder = 'Nome';
    columnName.type = 'text';

    var columnCountry = new GridColumnSetings();
    columnCountry.name = 'FKCountryName';
    columnCountry.title = 'País';
    columnCountry.sort = new GridSortSetings();
    columnCountry.filter = new GridFilterSetings();
    columnCountry.filter.filterString = '';
    columnCountry.filter.placeholder = 'País';
    columnCountry.type = 'text';

    var columnState = new GridColumnSetings();
    columnState.name = 'FKStateName';
    columnState.title = 'Estado';
    columnState.sort = new GridSortSetings();
    columnState.filter = new GridFilterSetings();
    columnState.filter.filterString = '';
    columnState.filter.placeholder = 'Estado';
    columnState.type = 'text';

    var columnCreateDate = new GridColumnSetings();
    columnCreateDate.name = 'CreateDate';
    columnCreateDate.title = 'Data inclusão';
    columnCreateDate.sort = new GridSortSetings();
    columnCreateDate.filter = new GridFilterSetings();
    columnCreateDate.filter.filterString = '';
    columnCreateDate.filter.placeholder = 'Data inclusão';
    columnCreateDate.type = 'date';

    var columnModifiedDate = new GridColumnSetings();
    columnModifiedDate.name = 'ModifiedDate';
    columnModifiedDate.title = 'Data Alteração';
    columnModifiedDate.sort = new GridSortSetings();
    columnModifiedDate.filter = new GridFilterSetings();
    columnModifiedDate.filter.filterString = '';
    columnModifiedDate.filter.placeholder = 'Data Alteração';
    columnModifiedDate.type = 'date';

    var columnStatus = new GridColumnSetings();
    columnStatus.name = 'Status';
    columnStatus.title = 'Status';
    columnStatus.sort = new GridSortSetings();
    columnStatus.filter = new GridFilterSetings();
    columnStatus.filter.filterString = '';
    columnStatus.filter.placeholder = 'Status';
    columnStatus.type = 'boolean';
    columnStatus.boolean = new BooleanType();
    columnStatus.boolean.true = 1;
    columnStatus.boolean.false = 2;

    var coluns = new Array<GridColumnSetings>();
    coluns.push(columnId);
    coluns.push(columnName);
    coluns.push(columnCreateDate);
    coluns.push(columnModifiedDate);
    coluns.push(columnState);
    coluns.push(columnCountry);
    coluns.push(columnStatus);

    this._gridSetings = new GridSetings();
    this._gridSetings.columns = coluns;
    this._gridSetings.form = this.form;
    this._gridSetings.context = this;
    this._gridSetings.crudPlugContext = this._crudplugComponent;

    this._crudPlugSetings = new CrudPlugSetings()
    this._crudPlugSetings.Url = this.url;
    this._crudPlugSetings.Eraser = true;
    this._crudPlugSetings.Add = true;
    this._crudPlugSetings.Edit = true;
    this._crudPlugSetings.Delete = true;
    this._crudPlugSetings.Search = true;
    this._crudPlugSetings.Refresh = true;
    this._crudPlugSetings.Grid = true;
    this._crudPlugSetings.Form = true;

    this._crudPlugSetings.gridSettings = this._gridSetings;
    this._crudPlugSetings.context = this;
    this._crudPlugSetings.formG = this.form;

    this.config = {
      paging: true,
      // sorting: { columns: this.columns }
    };


    this.DdlCountriesSetings.PropertyValue = 'Id';
    this.DdlCountriesSetings.PropertyText = 'Name';
    this.DdlCountriesSetings.Method = MethodEnum.Get;
    this.DdlCountriesSetings.ServiceMethodURL = urlBase + "/country/GetAll";

    this.DdlStatesSetings.PropertyValue = 'Id';
    this.DdlStatesSetings.PropertyText = 'Name';
    this.DdlStatesSetings.Method = MethodEnum.Post;
    let idCountry = this.form.get('IdCountry').value;
    if (idCountry > 0) {
      this.DdlStatesSetings.ServiceMethodURL = urlBase + "/state/GetByCountry";
      this.DdlStatesSetings.Parameter.Id = idCountry;
    }
  }
  DropDownCountryChange(obj: any) {
    if (obj.Id > 0) {
      this.DdlStatesSetings.Parameter = new RequestById();
      this.DdlStatesSetings.ServiceMethodURL = urlBase + "/state/GetByCountry";
      this.DdlStatesSetings.Parameter.Id = obj.Id;
      this.ddlState.Reload();
    }
  }

  ngAfterViewInit() {
    let context = this;

  }

  changeDataForm(NewCurrentItem) {
    if (NewCurrentItem.data.CreateDate != null) {
      NewCurrentItem.data.CreateDate = NewCurrentItem.data.CreateDate.substring(0, 10);
    }
    if (NewCurrentItem.data.ModifiedDate != null) {
      NewCurrentItem.data.ModifiedDate = NewCurrentItem.data.ModifiedDate.substring(0, 10);
    }
    this.form.patchValue(NewCurrentItem.data);
    this.currentItem.CreateDate = this.form.value.CreateDate;
    this.currentItem.ModifiedDate = this.form.value.ModifiedDate;
    this.currentItem.IdCountry = this.form.value.IdCountry;
    this.currentItem.IdState = this.form.value.IdState;
    this.currentItem.Id = this.form.value.Id;
    this.currentItem.Name = this.form.value.Name;
    this.currentItem.Status = this.form.value.Status;
  }

  rowClick(val: City) {
    this.currentItem = val;
  }
  cleanForm(val) {
    // this.form = this.currentItem.initForm();
  }
}  
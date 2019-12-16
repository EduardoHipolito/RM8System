import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { Observable } from 'rxjs';

import { GridComponent } from '../../_framework/grid/grid.component';
import { GridFilteringDirective } from '../../_framework/grid/grid-filtering.directive';
import { GridPagingDirective } from '../../_framework/grid/grid-paging.directive';
import { GridSortingDirective } from '../../_framework/grid/grid-sorting.directive';
import { baseCrudComponent } from '../../_framework/helppers/baseCrud.component';

import { NotificationsService } from '../../_framework/notification/notifications.service';
import { UserAplicationCompany } from '../../_domains/UserAplicationCompany';
import { urlBase } from '../../_framework/helppers/configs';
import { GridSetings, GridColumnSetings, GridSortSetings, GridFilterSetings, BooleanType } from '../../_framework/grid/GridSetings';
import { CrudPlugSetings } from '../../_framework/crudplug/CrudPlugSetings';
import { EntityType } from '../../_domains/enums/EntityType';
import { DropDownSetings, MethodEnum } from '../../_framework/dropdown/DropDownSetings';
import { AccessLevel } from '../../_domains/enums/AccessLevel';
import { Methods } from '../../_framework/helppers/methods';

@Component({
  selector: 'useraplicationcompany',
  templateUrl: './useraplicationcompany.component.html',
  styleUrls: ['./useraplicationcompany.component.css']
})
export class UserAplicationCompanyComponent extends baseCrudComponent  {
  public url: string = urlBase + "/useraplicationcompany";
  public currentItem: UserAplicationCompany;
  public form: FormGroup;

  public _gridSetings: GridSetings;
  public _crudPlugSetings: CrudPlugSetings;
  DdlAplicationsSetings: DropDownSetings = new DropDownSetings();
  DdlAccessLevelSetings: DropDownSetings = new DropDownSetings();
  DdlUsersSetings: DropDownSetings = new DropDownSetings();
  DdlCompanySetings: DropDownSetings = new DropDownSetings();

  constructor(private _fb: FormBuilder, private notificationsService: NotificationsService) {
    super();
  }

  public initForm() {
    return this._fb.group({
      Id: [null],
      CreateDate: [null],
      ModifiedDate: [null],
      Status: [EntityType.Ativo, Validators.required],
      IdAplication: [null, Validators.required],
      AccessLevel: [1,Validators.required],
      IdUser: [null, Validators.required],
      IdCompany: [null, Validators.required]
    });
  }

  InitializeComponent() {
    this.currentItem = new UserAplicationCompany();
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

    var columnUserName = new GridColumnSetings();
    columnUserName.name = 'FKUserName';
    columnUserName.title = 'Usuario';
    columnUserName.sort = new GridSortSetings();
    columnUserName.filter = new GridFilterSetings();
    columnUserName.filter.filterString = '';
    columnUserName.filter.placeholder = 'Usuario';
    columnUserName.type = 'text';

    var columnName = new GridColumnSetings();
    columnName.name = 'FKCompanyName';
    columnName.title = 'Empresa';
    columnName.sort = new GridSortSetings();
    columnName.filter = new GridFilterSetings();
    columnName.filter.filterString = '';
    columnName.filter.placeholder = 'Empresa';
    columnName.type = 'text';

    var columnDescription = new GridColumnSetings();
    columnDescription.name = 'FKAplicationName';
    columnDescription.title = 'Aplicação';
    columnDescription.sort = new GridSortSetings();
    columnDescription.filter = new GridFilterSetings();
    columnDescription.filter.filterString = '';
    columnDescription.filter.placeholder = 'Aplicação';
    columnDescription.type = 'text';

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
    coluns.push(columnUserName);
    coluns.push(columnName);
    coluns.push(columnDescription);
    coluns.push(columnCreateDate);
    coluns.push(columnModifiedDate);
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

    this.DdlAplicationsSetings.PropertyValue = 'Id';
    this.DdlAplicationsSetings.PropertyText = 'Name';
    this.DdlAplicationsSetings.Method = MethodEnum.Get;
    this.DdlAplicationsSetings.ServiceMethodURL = urlBase + "/aplication/GetAll";

    this.DdlUsersSetings.PropertyValue = 'Id';
    this.DdlUsersSetings.PropertyText = 'Login';
    this.DdlUsersSetings.Method = MethodEnum.Get;
    this.DdlUsersSetings.ServiceMethodURL = urlBase + "/user/GetAll";

    this.DdlCompanySetings.PropertyValue = 'Id';
    this.DdlCompanySetings.PropertyText = 'ReducedName';
    this.DdlCompanySetings.Method = MethodEnum.Get;
    this.DdlCompanySetings.ServiceMethodURL = urlBase + "/company/GetAll";

    let typeList = Methods.EnumToArray(AccessLevel);
    this.DdlAccessLevelSetings.DataSource = typeList;

    this.config = {
      paging: true,
      // sorting: { columns: this.columns }
    };
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
    this.currentItem.IdAplication = this.form.value.IdAplication;
    this.currentItem.IdUser = this.form.value.IdUser;
    this.currentItem.IdCompany = this.form.value.IdCompany;
    this.currentItem.Id = this.form.value.Id;
    this.currentItem.Status = this.form.value.Status;
  }

  rowClick(val: UserAplicationCompany) {
    this.currentItem = val;
  }
  cleanForm(val) {
    // this.form = this.currentItem.initForm();
  }
}  
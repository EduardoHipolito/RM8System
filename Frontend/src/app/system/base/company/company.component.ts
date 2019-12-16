import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { Observable } from 'rxjs';

import { GridComponent } from '../../_framework/grid/grid.component';
import { GridFilteringDirective } from '../../_framework/grid/grid-filtering.directive';
import { GridPagingDirective } from '../../_framework/grid/grid-paging.directive';
import { GridSortingDirective } from '../../_framework/grid/grid-sorting.directive';
import { baseCrudComponent } from '../../_framework/helppers/baseCrud.component';

import { NotificationsService } from '../../_framework/notification/notifications.service';
import { Company } from '../../_domains/Company';
import { urlBase } from '../../_framework/helppers/configs';
import { GridSetings, GridColumnSetings, GridSortSetings, GridFilterSetings, BooleanType } from '../../_framework/grid/GridSetings';
import { CrudPlugSetings } from '../../_framework/crudplug/CrudPlugSetings';
import { EntityType } from '../../_domains/enums/EntityType';
import { CompanyType } from '../../_domains/enums/CompanyType';
import { DropDownSetings, MethodEnum } from '../../_framework/dropdown/DropDownSetings';
import { Methods } from '../../_framework/helppers/methods';

@Component({
  selector: 'company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})
export class CompanyComponent extends baseCrudComponent {
  public url: string = urlBase + "/company";
  public currentItem: Company;
  public form: FormGroup;

  public _gridSetings: GridSetings;
  public _crudPlugSetings: CrudPlugSetings;

  DdlCompanyTypeSetings: DropDownSetings = new DropDownSetings();
  DdlPersonSetings: DropDownSetings = new DropDownSetings();
  DdlMasterSetings: DropDownSetings = new DropDownSetings();

  constructor(private _fb: FormBuilder, private notificationsService: NotificationsService) {
    super();
  }

  public initForm() {
    return this._fb.group({
      Id: [null],
      CreateDate: [null],
      ModifiedDate: [null],
      Status: [EntityType.Ativo, Validators.required],
      ReducedName: ['', Validators.required],
      PaymentDay: [null, Validators.required],
      Type: [CompanyType.Matriz, Validators.required],
      IdPerson: [null, Validators.required],
      IdMaster: [null],
    });
  }

  InitializeComponent() {
    this.currentItem = new Company();
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

    var columnFKPerson = new GridColumnSetings();
    columnFKPerson.title = 'Nome Fantazia';
    columnFKPerson.name = 'FKPerson.FantasyName';
    columnFKPerson.sort = new GridSortSetings();
    columnFKPerson.filter = new GridFilterSetings();
    columnFKPerson.filter.filterString = '';
    columnFKPerson.filter.placeholder = 'Nome Fantazia';
    columnFKPerson.type = 'text';

    var columnReducedName = new GridColumnSetings();
    columnReducedName.title = 'Nome';
    columnReducedName.name = 'ReducedName';
    columnReducedName.sort = new GridSortSetings();
    columnReducedName.filter = new GridFilterSetings();
    columnReducedName.filter.filterString = '';
    columnReducedName.filter.placeholder = 'Nome';
    columnReducedName.type = 'text';

    var columnPaymentDay = new GridColumnSetings();
    columnPaymentDay.title = 'Vencimento';
    columnPaymentDay.name = 'PaymentDay';
    columnPaymentDay.sort = new GridSortSetings();
    columnPaymentDay.filter = new GridFilterSetings();
    columnPaymentDay.filter.filterString = '';
    columnPaymentDay.filter.placeholder = 'Vencimento';
    columnPaymentDay.type = 'text';

    var columnFKMaster = new GridColumnSetings();
    columnFKMaster.name = 'FKMaster ? FKMaster.ReducedName: ';
    columnFKMaster.title = 'Superior';
    columnFKMaster.sort = new GridSortSetings();
    columnFKMaster.filter = new GridFilterSetings();
    columnFKMaster.filter.filterString = '';
    columnFKMaster.filter.placeholder = 'Superior';
    columnFKMaster.type = 'text';

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
    coluns.push(columnFKPerson);
    coluns.push(columnReducedName);
    coluns.push(columnFKMaster);
    coluns.push(columnPaymentDay);
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

    this.config = {
      paging: true,
      // sorting: { columns: this.columns }
    };

    
    this.DdlPersonSetings.PropertyValue = 'Id';
    this.DdlPersonSetings.PropertyText = 'FantasyName';
    this.DdlPersonSetings.Method = MethodEnum.Get;
    this.DdlPersonSetings.ServiceMethodURL = urlBase + "/legalperson/GetAll";

    this.DdlMasterSetings.PropertyValue = 'Id';
    this.DdlMasterSetings.PropertyText = 'ReducedName';
    this.DdlMasterSetings.Method = MethodEnum.Get;
    this.DdlMasterSetings.ServiceMethodURL = urlBase + "/company/GetAllMasters";

    let companyTypeList = Methods.EnumToArray(CompanyType);
    this.DdlCompanyTypeSetings.DataSource = companyTypeList;
  }

  DropDownCompanyTypeChange(obj: any) {

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
    this.currentItem.Type = this.form.value.Type;
    this.currentItem.PaymentDay = this.form.value.PaymentDay;
    this.currentItem.Id = this.form.value.Id;
    this.currentItem.IdPerson = this.form.value.IdPerson;
    this.currentItem.IdMaster = this.form.value.IdMaster;
    this.currentItem.ReducedName = this.form.value.ReducedName;
    this.currentItem.Status = this.form.value.Status;
  }

  rowClick(val: Company) {
    this.currentItem = val;
  }
  cleanForm(val) {
    // this.form = this.currentItem.initForm();
  }
}  
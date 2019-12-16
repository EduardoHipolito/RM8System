import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { Observable } from 'rxjs';

import { GridComponent } from '../../_framework/grid/grid.component';
import { GridFilteringDirective } from '../../_framework/grid/grid-filtering.directive';
import { GridPagingDirective } from '../../_framework/grid/grid-paging.directive';
import { GridSortingDirective } from '../../_framework/grid/grid-sorting.directive';
import { baseCrudComponent } from '../../_framework/helppers/baseCrud.component';

import { NotificationsService } from '../../_framework/notification/notifications.service';
import { Person } from '../../_domains/Person';
import { urlBase } from '../../_framework/helppers/configs';
import { Address } from '../../_domains/Address';
import { Document } from '../../_domains/Document';
import { Phone } from '../../_domains/Phone';
import { PersonType } from '../../_domains/enums/PersonType';
import { GridSetings, GridColumnSetings, GridSortSetings, GridFilterSetings, BooleanType } from '../../_framework/grid/GridSetings';
import { CrudPlugSetings } from '../../_framework/crudplug/CrudPlugSetings';
import { EntityType } from '../../_domains/enums/EntityType';
import { AddressType } from '../../_domains/enums/AddressType';
import { PublicAreaType } from '../../_domains/enums/PublicAreaType';
import { DropDownSetings } from '../../_framework/dropdown/DropDownSetings';
import { PhysicalPerson } from '../../_domains/PhysicalPerson';
import { Methods } from '../../_framework/helppers/methods';

@Component({
  selector: 'physicalperson',
  templateUrl: './physicalperson.component.html',
  styleUrls: ['./physicalperson.component.css']
})
export class PhysicalPersonComponent extends baseCrudComponent {
  public PersonTypeText: string = 'Fisica';
  public tabActive: string;
  public currentItem: PhysicalPerson;
  public form: FormGroup;

  public _gridSetings: GridSetings;
  public _crudPlugSetings: CrudPlugSetings;

  constructor(private _fb: FormBuilder, private notificationsService: NotificationsService) {
    super();
  }

  get Addresses(): FormArray {
    return this.form.get('Addresses') as FormArray;
  }
  get Documents(): FormArray {
    return this.form.get('Documents') as FormArray;
  }
  get Phones(): FormArray {
    return this.form.get('Phones') as FormArray;
  }
  public initForm() {
    return this._fb.group({
      Id: [null],
      CreateDate: [null],
      ModifiedDate: [null],
      Status: [EntityType.Ativo, Validators.required],
      Name: ['', Validators.required],
      HomePage: [''],
      Email: ['', Validators.required],
      Addresses: this._fb.array([
        this.createAddress()
      ]),
      Documents: this._fb.array([
        this.createDocument()
      ]),
      Phones: this._fb.array([
        this.createPhone()
      ])
    });
  }
  createAddress(): FormGroup {
    return this._fb.group({
      Id: [null],
      CreateDate: [null],
      ModifiedDate: [null],
      Status: [EntityType.Ativo, Validators.required],
      IdPessoa: [null],
      Type: [AddressType.Residencial, Validators.required],
      PublicAreaType: [PublicAreaType.Rua, Validators.required],
      PublicArea: ['', Validators.required],
      Complement: [''],
      Number: [null, Validators.required],
      Neighborhood: ['', Validators.required],
      PostalCode: [null, Validators.required],
      IdCountry: [null, Validators.required],
      IdState: [null, Validators.required],
      IdCity: [null, Validators.required],
    });
  }
  createDocument(): FormGroup {
    return this._fb.group({
      Id: [null],
      CreateDate: [null],
      ModifiedDate: [null],
      Status: [EntityType.Ativo, Validators.required],
      IdPerson: [null],
      IdDocumentType: [null, Validators.required],
      Value: ['', Validators.required],
    });
  }
  createPhone(): FormGroup {
    return this._fb.group({
      Id: [null],
      CreateDate: [null],
      ModifiedDate: [null],
      Status: [EntityType.Ativo, Validators.required],
      IdPerson: [null],
      Type: [Validators.required],
      IdCountry: [null, Validators.required],
      AreaCode: [null, Validators.required],
      Number: [null, Validators.required],
    });
  }

  InitializeComponent() {

    let typeList = Methods.EnumToArray(EntityType);
    this.DdlStatusSetings.DataSource = typeList;

    this.tabActive = "Close";
    this.currentItem = new Person();
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

    var columnHomePage = new GridColumnSetings();
    columnHomePage.name = 'HomePage';
    columnHomePage.title = 'Home Page';
    columnHomePage.sort = new GridSortSetings();
    columnHomePage.filter = new GridFilterSetings();
    columnHomePage.filter.filterString = '';
    columnHomePage.filter.placeholder = 'Home Page';
    columnHomePage.type = 'text';

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

    var columnModal = new GridColumnSetings();
    columnModal.title = '';
    columnModal.sort = new GridSortSetings();
    columnModal.sort.able = false;
    columnModal.type = 'modal';
    columnModal.modalId = 'addressModal';

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
    coluns.push(columnHomePage);
    coluns.push(columnCreateDate);
    coluns.push(columnModifiedDate);
    coluns.push(columnModal);
    coluns.push(columnStatus);

    this._gridSetings = new GridSetings();
    this._gridSetings.columns = coluns;
    this._gridSetings.form = this.form;
    this._gridSetings.context = this;
    this._gridSetings.crudPlugContext = this._crudplugComponent;

    this._crudPlugSetings = new CrudPlugSetings()
    this._crudPlugSetings.Url = urlBase + "/PhysicalPerson";
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
    this.currentItem.Addresses = this.form.value.Addresses;
    this.currentItem.Documents = this.form.value.Documents;
    this.currentItem.Phones = this.form.value.Phones;
    this.currentItem.CreateDate = this.form.value.CreateDate;
    this.currentItem.ModifiedDate = this.form.value.ModifiedDate;
    this.currentItem.Email = this.form.value.Email;
    this.currentItem.HomePage = this.form.value.HomePage;
    this.currentItem.Id = this.form.value.Id;
    this.currentItem.Name = this.form.value.Name;
    this.currentItem.Status = this.form.value.Status;
  }

  AddAddress() {
    this.Addresses.push(this.createAddress());
  }
  RemoveAddress(item: FormGroup) {
    var idx = this.Addresses.controls.indexOf(item);
    this.Addresses.removeAt(idx);
  }

  AddDocument() {
    this.Documents.push(this.createDocument());
  }
  RemoveDocument(item: FormGroup) {
    var idx = this.Documents.controls.indexOf(item);
    this.Documents.removeAt(idx);
  }

  AddPhone() {
    this.Phones.push(this.createPhone());
  }
  RemovePhone(item: FormGroup) {
    var idx = this.Phones.controls.indexOf(item);
    this.Phones.removeAt(idx);
  }
  rowClick(val: Person) {
    this.currentItem = val;
  }
  cleanForm(val) {
    // this.form = this.currentItem.initForm();
  }

  tabChange(val: string) {
    this.tabActive = val;
  }

}  
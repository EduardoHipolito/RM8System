import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { Observable } from 'rxjs';

import { GridComponent } from '../../_framework/grid/grid.component';
import { GridFilteringDirective } from '../../_framework/grid/grid-filtering.directive';
import { GridPagingDirective } from '../../_framework/grid/grid-paging.directive';
import { GridSortingDirective } from '../../_framework/grid/grid-sorting.directive';
import { baseCrudComponent } from '../../_framework/helppers/baseCrud.component';

import { NotificationsService } from '../../_framework/notification/notifications.service';
import { Product } from '../../_domains/Product';
import { urlStock, urlBase } from '../../_framework/helppers/configs';
import { GridSetings, GridColumnSetings, GridSortSetings, GridFilterSetings, BooleanType } from '../../_framework/grid/GridSetings';
import { CrudPlugSetings } from '../../_framework/crudplug/CrudPlugSetings';
import { EntityType } from '../../_domains/enums/EntityType';
import { DropDownSetings, MethodEnum } from '../../_framework/dropdown/DropDownSetings';
import { ProductUnityType } from '../../_domains/enums/ProductUnityType';
import { Methods } from '../../_framework/helppers/methods';
import { ProductType } from '../../_domains/enums/ProductType';

@Component({
  selector: 'product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent extends baseCrudComponent {
  public url: string = urlStock + "/product";
  public currentItem: Product;
  public form: FormGroup;

  public _gridSetings: GridSetings;
  public _crudPlugSetings: CrudPlugSetings;
  DdlSupplierSetings: DropDownSetings = new DropDownSetings();
  DdlUnityTypeSetings: DropDownSetings = new DropDownSetings();
  DdlProductTypeSetings: DropDownSetings = new DropDownSetings();
  DdlCategorySetings: DropDownSetings = new DropDownSetings();


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
      BarCode: ['', Validators.required],
      Packing: ['', Validators.required],
      Color: [''],
      MoreInformation: [''],
      Weight: [0],
      CostPrice: [0, Validators.required],
      Price: [0, Validators.required],
      MinPrice: [0, Validators.required],
      InternalNumber: [''],
      Description: [''],
      UnityType: [ProductUnityType.Unitario, Validators.required],
      IdCategory: [null, Validators.required],
      Brand: [''],
      Picture: [null],
      ProductType: [ProductType.Normal, Validators.required],

    });
  }

  InitializeComponent() {
    this.currentItem = new Product();
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

    var columnPrice = new GridColumnSetings();
    columnPrice.name = 'Price';
    columnPrice.title = 'Preço';
    columnPrice.sort = new GridSortSetings();
    columnPrice.filter = new GridFilterSetings();
    columnPrice.filter.filterString = '';
    columnPrice.filter.placeholder = 'Preço';
    columnPrice.type = 'currency';

    var columnCostPrice = new GridColumnSetings();
    columnCostPrice.name = 'CostPrice';
    columnCostPrice.title = 'Preço de custo';
    columnCostPrice.sort = new GridSortSetings();
    columnCostPrice.filter = new GridFilterSetings();
    columnCostPrice.filter.filterString = '';
    columnCostPrice.filter.placeholder = 'Preço de custo';
    columnCostPrice.type = 'currency';

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
    coluns.push(columnPrice);
    coluns.push(columnCostPrice);
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

    this.DdlCategorySetings.PropertyValue = 'Id';
    this.DdlCategorySetings.PropertyText = 'Name';
    this.DdlCategorySetings.Method = MethodEnum.Get;
    this.DdlCategorySetings.ServiceMethodURL = urlStock + "/category/GetAll";

    let productTypeList = Methods.EnumToArray(ProductType);
    this.DdlProductTypeSetings.DataSource = productTypeList;

    let typeList = Methods.EnumToArray(ProductUnityType);
    this.DdlUnityTypeSetings.DataSource = typeList;

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
    this.currentItem.Id = this.form.value.Id;
    this.currentItem.MoreInformation = this.form.value.MoreInformation;
    this.currentItem.Status = this.form.value.Status;
    this.currentItem.Name = this.form.value.Name;
    this.currentItem.Color = this.form.value.Color;
    this.currentItem.Description = this.form.value.Description;
    this.currentItem.IdCategory = this.form.value.IdCategory;
    this.currentItem.Brand = this.form.value.Brand;
    this.currentItem.InternalNumber = this.form.value.InternalNumber;
    this.currentItem.BarCode = this.form.value.BarCode;
    this.currentItem.UnityType = this.form.value.UnityType;
    this.currentItem.Packing = this.form.value.Packing;
    this.currentItem.CostPrice = this.form.value.CostPrice;
    this.currentItem.Weight = this.form.value.Weight;
    this.currentItem.Price = this.form.value.Price;
    this.currentItem.MinPrice = this.form.value.MinPrice;
    this.currentItem.ProductType = this.form.value.ProductType;
    this.currentItem.Picture = this.form.value.Picture;
  }

  rowClick(val: Product) {
    this.currentItem = val;
  }
  cleanForm(val) {
    // this.form = this.currentItem.initForm();
  }
  beforeCrudClick(){
    this.form.value.CostPrice = Methods.stringToNumber(this.form.value.CostPrice);
    this.form.value.Price = Methods.stringToNumber(this.form.value.Price);
    this.form.value.MinPrice = Methods.stringToNumber(this.form.value.MinPrice);
  }
}  
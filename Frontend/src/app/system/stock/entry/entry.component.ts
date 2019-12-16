import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { baseCrudComponent } from '../../_framework/helppers/baseCrud.component';

import { NotificationsService } from '../../_framework/notification/notifications.service';
import { Entry } from '../../_domains/Entry';
import { ProductEntry } from '../../_domains/ProductEntry';
import { urlStock, urlBase } from '../../_framework/helppers/configs';
import { GridSetings, GridColumnSetings, GridSortSetings, GridFilterSetings, BooleanType } from '../../_framework/grid/GridSetings';
import { CrudPlugSetings } from '../../_framework/crudplug/CrudPlugSetings';
import { EntityType } from '../../_domains/enums/EntityType';
import { DropDownSetings, MethodEnum } from '../../_framework/dropdown/DropDownSetings';
import { Methods } from '../../_framework/helppers/methods';

@Component({
  selector: 'entry',
  templateUrl: './entry.component.html',
  styleUrls: ['./entry.component.css']
})
export class EntryComponent extends baseCrudComponent {
  public url: string = urlStock + "/entry";
  public currentItem: Entry;
  public form: FormGroup;
  public tabActive: string;

  public _gridSetings: GridSetings;
  public _crudPlugSetings: CrudPlugSetings;
  DdlSupplierSetings: DropDownSetings = new DropDownSetings();


  constructor(private _fb: FormBuilder, private notificationsService: NotificationsService) {
    super();
  }


  tabChange(val: string) {
    console.log(val);
    this.tabActive = val;
  }
  public initForm() {
    return this._fb.group({
      Id: [null],
      CreateDate: [null],
      ModifiedDate: [null],
      Status: [EntityType.Ativo, Validators.required],
      IdSupplier: [null, Validators.required],
      TotalPrice: [0, Validators.required],
      Discount: [0],
      Shipping: [0],
      ProductEntries: this._fb.array([
        this._fb.group({
          Id: [null],
          CreateDate: [null],
          ModifiedDate: [null],
          Status: [EntityType.Ativo, Validators.required],
          IdEntry: [null],
          IdProduct: [null, Validators.required],
          UnitPrice: [0, Validators.required],
          ICMS: [0],
          IPI: [0],
          TotalPrice: [0],
          Quantity: [0, Validators.required]
        })
      ]),
    });
  }

  InitializeComponent() {
    this.currentItem = new Entry();
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

    var columnTotalPrice = new GridColumnSetings();
    columnTotalPrice.name = 'TotalPrice';
    columnTotalPrice.title = 'Valor total';
    columnTotalPrice.sort = new GridSortSetings();
    columnTotalPrice.filter = new GridFilterSetings();
    columnTotalPrice.filter.filterString = '';
    columnTotalPrice.filter.placeholder = 'Valor total';
    columnTotalPrice.type = 'currency';

    var columnDiscount = new GridColumnSetings();
    columnDiscount.name = 'Discount';
    columnDiscount.title = 'Desconto';
    columnDiscount.sort = new GridSortSetings();
    columnDiscount.filter = new GridFilterSetings();
    columnDiscount.filter.filterString = '';
    columnDiscount.filter.placeholder = 'Desconto';
    columnDiscount.type = 'currency';

    var columnShipping = new GridColumnSetings();
    columnShipping.name = 'Shipping';
    columnShipping.title = 'Frete';
    columnShipping.sort = new GridSortSetings();
    columnShipping.filter = new GridFilterSetings();
    columnShipping.filter.filterString = '';
    columnShipping.filter.placeholder = 'Frete';
    columnShipping.type = 'currency';

    var columnCreateDate = new GridColumnSetings();
    columnCreateDate.name = 'CreateDate';
    columnCreateDate.title = 'Data inclusão';
    columnCreateDate.sort = new GridSortSetings();
    columnCreateDate.filter = new GridFilterSetings();
    columnCreateDate.filter.filterString = '';
    columnCreateDate.filter.placeholder = 'Data inclusão';
    columnCreateDate.type = 'date';

    var columnModal = new GridColumnSetings();
    columnModal.title = '';
    columnModal.sort = new GridSortSetings();
    columnModal.sort.able = false;
    columnModal.type = 'modal';
    columnModal.modalId = 'productsModal';

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
    coluns.push(columnTotalPrice);
    coluns.push(columnShipping);
    coluns.push(columnDiscount);
    coluns.push(columnCreateDate);
    coluns.push(columnStatus);
    coluns.push(columnModal);

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

    this.DdlSupplierSetings.PropertyValue = 'Id';
    this.DdlSupplierSetings.PropertyText = 'FKLegalPerson.CorporateName';
    this.DdlSupplierSetings.Method = MethodEnum.Get;
    this.DdlSupplierSetings.ServiceMethodURL = urlStock + "/supplier/GetAll";

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
    this.currentItem.IdSupplier = this.form.value.IdSupplier;
    this.currentItem.Id = this.form.value.Id;
    this.currentItem.TotalPrice = this.form.value.TotalPrice;
    this.currentItem.Status = this.form.value.Status;
    this.currentItem.Discount = this.form.value.Discount;
    this.currentItem.Shipping = this.form.value.Shipping;
    this.currentItem.ProductEntries = this.form.value.ProductEntries;
  }

  rowClick(val: Entry) {
    this.currentItem = val;
  }
  cleanForm(val) {
    // this.form = this.currentItem.initForm();
  }
  beforeCrudClick() {
    this.form.value.Discount = Methods.stringToNumber(this.form.value.Discount);
    this.form.value.Shipping = Methods.stringToNumber(this.form.value.Shipping);
    this.form.value.TotalPrice = Methods.stringToNumber(this.form.value.TotalPrice);
    (<Array<ProductEntry>>this.form.value.ProductEntries).forEach(f => {
      f.ICMS = Methods.stringToNumber(f.ICMS);
      f.IPI = Methods.stringToNumber(f.IPI);
      f.UnitPrice = Methods.stringToNumber(f.UnitPrice);
    });
  }

  changeTotalValue(event) {

    var total = 0;
    (<Array<ProductEntry>>this.form.value.ProductEntries).forEach(f => {
      total += (Methods.stringToNumber(f.ICMS) + Methods.stringToNumber(f.IPI) + (Methods.stringToNumber(f.UnitPrice) * Methods.stringToNumber(f.Quantity)));
    });
    total += Methods.stringToNumber(this.form.value.Shipping);
    total -= Methods.stringToNumber(this.form.value.Discount);

    this.form.controls['TotalPrice'].setValue(total);
  }

}
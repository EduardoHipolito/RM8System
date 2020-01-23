import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { Observable } from 'rxjs';

import { GridComponent } from '../../_framework/grid/grid.component';
import { GridFilteringDirective } from '../../_framework/grid/grid-filtering.directive';
import { GridPagingDirective } from '../../_framework/grid/grid-paging.directive';
import { GridSortingDirective } from '../../_framework/grid/grid-sorting.directive';
import { baseCrudComponent } from '../../_framework/helppers/baseCrud.component';

import { NotificationsService } from '../../_framework/notification/notifications.service';
import { StockSum } from '../../_domains/StockSum';
import { urlStock } from '../../_framework/helppers/configs';
import { GridSetings, GridColumnSetings, GridSortSetings, GridFilterSetings, BooleanType } from '../../_framework/grid/GridSetings';
import { CrudPlugSetings } from '../../_framework/crudplug/CrudPlugSetings';
import { EntityType } from '../../_domains/enums/EntityType';
import { StockHistory } from '../../_domains/StockHistory';
import { StockSummaryService } from './stocksummary.service';
import { baseComponent } from '../../_framework/helppers/base.component';
import { StockType } from '../../_domains/enums/StockType';
import { ResponseResult } from '../../_framework/models/ResponseResult';

@Component({
  selector: 'stocksummary',
  templateUrl: './stocksummary.component.html',
  styleUrls: ['./stocksummary.component.css']
})
export class StockSummaryComponent extends baseCrudComponent {
  public url: string = urlStock + "/stock";
  public currentItem: StockSum;


  public _gridSetings: GridSetings;
  public _gridHistorySetings: GridSetings;

  constructor(private _fb: FormBuilder, private notificationsService: NotificationsService, private _stockSummaryService: StockSummaryService) {
    super();
  }

  FillGrid() {

    var columnPicture = new GridColumnSetings();
    columnPicture.name = 'Picture';
    columnPicture.title = 'Imagem';
    columnPicture.sort = new GridSortSetings();
    columnPicture.filter = new GridFilterSetings();
    columnPicture.filter.filterString = '';
    columnPicture.filter.placeholder = 'Imagem';
    columnPicture.type = 'image';
    columnPicture.alt = 'Foto';
    columnPicture.height = '50';
    // columnPicture.width = '50';

    var columnName = new GridColumnSetings();
    columnName.name = 'ProductName';
    columnName.title = 'Nome';
    columnName.sort = new GridSortSetings();
    columnName.filter = new GridFilterSetings();
    columnName.filter.filterString = '';
    columnName.filter.placeholder = 'Nome';
    columnName.type = 'text';

    var columnBrand = new GridColumnSetings();
    columnBrand.name = 'Brand';
    columnBrand.title = 'Marca';
    columnBrand.sort = new GridSortSetings();
    columnBrand.filter = new GridFilterSetings();
    columnBrand.filter.filterString = '';
    columnBrand.filter.placeholder = 'Marca';
    columnBrand.type = 'text';

    var columnInternalNumber = new GridColumnSetings();
    columnInternalNumber.name = 'InternalNumber';
    columnInternalNumber.title = 'Código Interno';
    columnInternalNumber.sort = new GridSortSetings();
    columnInternalNumber.filter = new GridFilterSetings();
    columnInternalNumber.filter.filterString = '';
    columnInternalNumber.filter.placeholder = 'Código Interno';
    columnInternalNumber.type = 'text';

    var columnQuantity = new GridColumnSetings();
    columnQuantity.name = 'Quantity';
    columnQuantity.title = 'Quantidade';
    columnQuantity.sort = new GridSortSetings();
    columnQuantity.filter = new GridFilterSetings();
    columnQuantity.filter.filterString = '';
    columnQuantity.filter.placeholder = 'Quantidade';
    columnQuantity.type = 'text';

    var columnCreateDate = new GridColumnSetings();
    columnCreateDate.name = 'CreateDate';
    columnCreateDate.title = 'Ultima alteração';
    columnCreateDate.sort = new GridSortSetings();
    columnCreateDate.filter = new GridFilterSetings();
    columnCreateDate.filter.filterString = '';
    columnCreateDate.filter.placeholder = 'Ultima alteração';
    columnCreateDate.type = 'date';

    var columnModal = new GridColumnSetings();
    columnModal.title = '';
    columnModal.sort = new GridSortSetings();
    columnModal.sort.able = false;
    columnModal.type = 'modal';
    columnModal.modalId = 'historyModal';
    columnModal.modalCallback = this.modalCalback;

    var coluns = new Array<GridColumnSetings>();
    coluns.push(columnPicture);
    coluns.push(columnName);
    coluns.push(columnBrand);
    coluns.push(columnInternalNumber);
    coluns.push(columnQuantity);
    coluns.push(columnCreateDate);
    coluns.push(columnModal);

    this._gridSetings = new GridSetings();
    this._gridSetings.columns = coluns;
    this._gridSetings.context = this;

    let context = this;
    this._stockSummaryService.GetSummary()
      .then(
        response => {
          var res = <ResponseResult>response;
          this._getAllCallBack(res.Data, context);
          this.notificationsService.success('Total de itens:' + res.Data.length);
        },
        error => { }
      );
  }

  FillHistoryGrid() {
    var columnName = new GridColumnSetings();
    columnName.name = 'ProductName';
    columnName.title = 'Produto';
    columnName.sort = new GridSortSetings();
    columnName.filter = new GridFilterSetings();
    columnName.filter.filterString = '';
    columnName.filter.placeholder = 'Produto';
    columnName.type = 'text';

    var columnSupplierName = new GridColumnSetings();
    columnSupplierName.name = 'SupplierName';
    columnSupplierName.title = 'Fornecedor';
    columnSupplierName.sort = new GridSortSetings();
    columnSupplierName.filter = new GridFilterSetings();
    columnSupplierName.filter.filterString = '';
    columnSupplierName.filter.placeholder = 'Fornecedor';
    columnSupplierName.type = 'text';

    var columnCustomerName = new GridColumnSetings();
    columnCustomerName.name = 'CustomerName';
    columnCustomerName.title = 'Cliente';
    columnCustomerName.sort = new GridSortSetings();
    columnCustomerName.filter = new GridFilterSetings();
    columnCustomerName.filter.filterString = '';
    columnCustomerName.filter.placeholder = 'Cliente';
    columnCustomerName.type = 'text';

    var columnStockType = new GridColumnSetings();
    columnStockType.name = 'StockType';
    columnStockType.title = 'Tipo';
    columnStockType.sort = new GridSortSetings();
    columnStockType.filter = new GridFilterSetings();
    columnStockType.filter.filterString = '';
    columnStockType.filter.placeholder = 'Tipo';
    columnStockType.type = 'custom';
    columnStockType.customRender = this.StockTypeCustomRender

    var columnQuantity = new GridColumnSetings();
    columnQuantity.name = 'Quantity';
    columnQuantity.title = 'Quantidade';
    columnQuantity.sort = new GridSortSetings();
    columnQuantity.filter = new GridFilterSetings();
    columnQuantity.filter.filterString = '';
    columnQuantity.filter.placeholder = 'Quantidade';
    columnQuantity.type = 'text';

    var columnCreateDate = new GridColumnSetings();
    columnCreateDate.name = 'CreateDate';
    columnCreateDate.title = 'Data';
    columnCreateDate.sort = new GridSortSetings();
    columnCreateDate.filter = new GridFilterSetings();
    columnCreateDate.filter.filterString = '';
    columnCreateDate.filter.placeholder = 'Data';
    columnCreateDate.type = 'date';


    var coluns = new Array<GridColumnSetings>();
    coluns.push(columnName);
    coluns.push(columnSupplierName);
    coluns.push(columnCustomerName);
    coluns.push(columnStockType);
    coluns.push(columnQuantity);
    coluns.push(columnCreateDate);

    this._gridHistorySetings = new GridSetings();
    this._gridHistorySetings.columns = coluns;
    this._gridHistorySetings.context = this;
  }

  InitializeComponent() {
    this.currentItem = new StockSum();

    this.FillGrid();
    this.FillHistoryGrid();


    this.config = {
      paging: true,
      // sorting: { columns: this.columns }
    };

  }

  _getAllCallBack(model: any, context: StockSummaryComponent) {
    context._gridSetings.data = model;
    context._gridSetings.rows = model;
  }

  _getAllHistoryCallBack(model: any, context: StockSummaryComponent) {
    context._gridHistorySetings.data = model;
    context._gridHistorySetings.rows = model;
  }
  ngAfterViewInit() {
    let context = this;
  }

  rowClick(val: StockSum) {
    this.currentItem = val;
  }
  cleanForm(val) {
    // this.form = this.currentItem.initForm();
  }

  modalCalback(Id: number, context: StockSummaryComponent, modalId: string, FinishAndOpenModalCallback: (modalId: string) => void) {

    context._stockSummaryService.GetHistoryByIdProduct(Id)
      .then(
        response => {
          var res = <ResponseResult>response;
          context._getAllHistoryCallBack(res.Data, context);
          FinishAndOpenModalCallback(modalId);
        },
        error => { }
      );
  }

  StockTypeCustomRender(row: StockHistory, context: baseComponent): string {

    switch (row.StockType) {
      case StockType.Acerto_De_Estoque:
        return "Ac. estoque";
      case StockType.Cancelamento_De_Entrada:
        return "Canc. entrada";
      case StockType.Cancelamento_De_Venda:
        return "Canc. venda";
      case StockType.Entrada:
        return "Entrada";
      case StockType.Venda:
        return "Venda";

      default:
        return "??";
    }
  }
}  
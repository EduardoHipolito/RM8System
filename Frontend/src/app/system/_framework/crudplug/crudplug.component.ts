import { Input, Output, EventEmitter, Component, AfterViewInit } from '@angular/core';

import { CrudplugService } from './crudplug.service';
import { NotificationsService } from './../notification/notifications.service';
import { CrudPlugSetings } from './CrudPlugSetings';
import { ValidationErrors } from '@angular/forms';
import { CrudPlugbuttonsType } from './CrudPlugbuttonsType';
import { ResponseResult } from '../models/ResponseResult';

@Component({
  selector: 'CrudPlug',
  templateUrl: './crudplug.component.html',
  styleUrls: ['./crudplug.component.css']
})
export class CrudplugComponent implements AfterViewInit {

  @Output() cleanForm = new EventEmitter();
  @Output() beforeCrudClick = new EventEmitter();
  @Input() public setings: CrudPlugSetings = new CrudPlugSetings();
  public EraserAble: string = "disabled";
  public AddAble: string = "disabled";
  public EditAble: string = "disabled";
  public DeleteAble: string = "disabled";
  public SearchAble: string = "disabled";
  public RefreshAble: string = "disabled";
  public GridFormAble: string = "disabled";
  public GridFormClassStatus: string = "fa fa-th";

  public butons: any = {
    Add: false,
    Eraser: false,
    Edit: false,
    Delete: false,
    Search: false,
    Refresh: false,
    AddAble: false,
    GridForm: false
  };


  constructor(private notificationsService: NotificationsService, private _service: CrudplugService) {
    this.butons.GridForm = true;
  }

  ngAfterViewInit() {
    this._service.UrlBase = this.setings.Url;
    this._service.SetUrlBase(this.setings.Url);
    this.onCrudClick(CrudPlugbuttonsType.Refresh);
    this.butons = {
      Eraser: this.setings.Eraser,
      Add: this.setings.Add,
      Edit: this.setings.Edit,
      Delete: this.setings.Delete,
      Search: this.setings.Search,
      Refresh: this.setings.Refresh,
      GridForm: (this.setings.Grid == true && this.setings.Form == true)
    };
    if (this.setings.Grid == true && this.setings.Form == true) {
      $('grid').hide();
      $('form').show();
    } else {
      if (this.setings.Grid == true) {
        $('grid').show();
      }
      if (this.setings.Form == true) {
        $('form').show();
      }
    }
    this.ChangeFormState("New");
  }

  GridForm() {
    if (this.GridFormClassStatus == "fa fa-th") {
      this.GridFormClassStatus = "fa fa-list";
      $('form').hide();
      $('grid').show();
    }
    else {
      this.GridFormClassStatus = "fa fa-th"
      $('grid').hide();
      $('form').show();
    }
  }

  getFormValidationErrors() {
    Object.keys(this.setings.formG.controls).forEach(key => {
      const controlErrors: ValidationErrors = this.setings.formG.get(key).errors;
      if (controlErrors != null) {
        Object.keys(controlErrors).forEach(keyError => {
          this.notificationsService.error('Key control: ' + key + ', keyError: ' + keyError + ', err value: ', controlErrors[keyError]);
        });
      }
    });
  }

  public onCrudClick(type: CrudPlugbuttonsType) {
    this.beforeCrudClick.next();
    switch (type) {
      case CrudPlugbuttonsType.Add:
        {
          if (this.setings.formG.valid) {
            this._service.Add(this.setings.formG.value)
              .then(
                response => {
                  let res = <ResponseResult>response;
                  this.setings.formEntity = res.Data;
                  this.ChangeFormState("Exist");
                  this.notificationsService.success('Inserido');
                  this.onCrudClick(CrudPlugbuttonsType.Refresh);
                },
                error => { }
              );
          } else {
            this.notificationsService.error('Existem campos invalidos');
          }
          break;
        }
      case CrudPlugbuttonsType.Edit:
        {
          if (this.setings.formG.valid) {
            this._service.Update(this.setings.formG.value)
              .then(
                res => {
                  this.notificationsService.success('Atualizado');
                  this.onCrudClick(CrudPlugbuttonsType.Refresh);
                },
                error => { }
              );
          } else {
            this.notificationsService.error('Existem campos invalidos');
          }
          break;
        }
      case CrudPlugbuttonsType.Delete:
        {
          this._service.Delete(this.setings.formG.value)
            .then(
              () => {
                this.cleanForm.next();
                this.ChangeFormState("New");
                this.notificationsService.success('Deletado');
                this.onCrudClick(CrudPlugbuttonsType.Refresh);
              },
              error => { }
            );
          break;
        }
      case CrudPlugbuttonsType.Search:
        {
          this._service.FindAll(this.setings.gridSettings.RequestSettings, this.setings.formG.value)
            .then(
              response => {
                let res = <ResponseResult>response;
                this.setings.formEntity = res.Data[0];
                this.setings.gridSettings.data = res.Data.DataList;
                this.setings.gridSettings.rows = res.Data.DataList;
                this.setings.gridSettings.TotalItems = res.Data.TotalRecords;
                this.setings.gridSettings.gridComponentOnLoadCallback != null ? this.setings.gridSettings.gridComponentOnLoadCallback(this.setings.gridSettings.gridContext) : null;
                this.setings.getAllCallBack != null ? this.setings.getAllCallBack(res.Data, this.setings.context, res.Type) : null;
                this.ChangeFormState("Exist");
                this.notificationsService.success('Total de itens: ' + res.Data.TotalRecords);
              },
              error => { }
            );
          break;
        }
      case CrudPlugbuttonsType.Refresh:
        {
          this._service.GetAllGrid(this.setings.gridSettings.RequestSettings)
            .then(
              response => {
                let res = <ResponseResult>response;
                this.setings.gridSettings.data = res.Data.DataList;
                this.setings.gridSettings.rows = res.Data.DataList;
                this.setings.gridSettings.TotalItems = res.Data.TotalRecords;
                this.setings.gridSettings.gridComponentOnLoadCallback != null ? this.setings.gridSettings.gridComponentOnLoadCallback(this.setings.gridSettings.gridContext) : null;
                this.setings.getAllCallBack != null ? this.setings.getAllCallBack(res.Data, this.setings.context, res.Type) : null;
                this.cleanForm.next();
                this.ChangeFormState("New");
                this.notificationsService.success('Total de itens: ' + res.Data.TotalRecords);
              },
              error => { }
            );
          break;
        }
      case CrudPlugbuttonsType.Eraser:
        {
          this.setings.formG.reset({});
          this.cleanForm.next();
          this.ChangeFormState("New");
          this.notificationsService.info('Formulario limpo');
          this.onCrudClick(CrudPlugbuttonsType.Refresh);
          break;
        }
    }
  }

  public ChangeFormState(state) {
    switch (state) {
      case 'New':
        {
          this.EraserAble = "";
          this.AddAble = "";
          this.EditAble = "disabled";
          this.DeleteAble = "disabled";
          this.SearchAble = "";
          this.RefreshAble = "";
          this.GridFormAble = "";
          break;
        }
      case 'Exist':
        {
          this.EraserAble = "";
          this.AddAble = "disabled";
          this.EditAble = "";
          this.DeleteAble = "";
          this.SearchAble = "disabled";
          this.RefreshAble = "";
          this.GridFormAble = "";
          break;
        }
    }
  }
}

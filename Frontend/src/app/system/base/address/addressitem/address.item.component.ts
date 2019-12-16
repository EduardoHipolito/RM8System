import { Component, ViewEncapsulation, HostListener, EventEmitter, Input, Output, OnInit, OnChanges, ViewChildren, QueryList, ViewChild } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Country } from '../../../_domains/Country';
import { State } from '../../../_domains/State';
import { DropDownSetings, MethodEnum } from '../../../_framework/dropdown/DropDownSetings';
import { NotificationsService } from '../../../_framework/notification/notifications.service';
import { urlBase } from '../../../_framework/helppers/configs';
import { RequestById } from '../../../_framework/models/RequestById';
import { DropDownComponent } from '../../../_framework/dropdown/dropdown.component';
import { City } from '../../../_domains/City';
import { baseComponent } from '../../../_framework/helppers/base.component';
import { AddressType } from '../../../_domains/enums/AddressType';
import { PublicAreaType } from '../../../_domains/enums/PublicAreaType';
import { Methods } from '../../../_framework/helppers/methods';


@Component({
  selector: 'addressitem',
  templateUrl: './address.item.component.html',
  styleUrls: ['./address.item.component.css']
})
export class AddressItemComponent extends baseComponent implements OnInit {

  @Input() formItem: FormGroup;
  @Input() public groupName: string;
  @Output() Remove: EventEmitter<any> = new EventEmitter();

  public addressText: string;
  DdlCountrySetings: DropDownSetings = new DropDownSetings();
  DdlStateSetings: DropDownSetings = new DropDownSetings();
  DdlCitySetings: DropDownSetings = new DropDownSetings();
  DdlTypeSetings: DropDownSetings = new DropDownSetings();
  DdlPublicAreaTypeSetings: DropDownSetings = new DropDownSetings();

  @ViewChild('ddlCountry') ddlCountry: DropDownComponent;
  @ViewChild('ddlState') ddlState: DropDownComponent;
  @ViewChild('ddlCity') ddlCity: DropDownComponent;

  @Input() public formG: FormGroup;
  constructor(private _notificationsService: NotificationsService) {
    super();
  }

  ngOnInit() {

    this.DdlCountrySetings.PropertyValue = 'Id';
    this.DdlCountrySetings.PropertyText = 'Name';
    this.DdlCountrySetings.Method = MethodEnum.Get;
    this.DdlCountrySetings.ServiceMethodURL = urlBase + "/country/GetAll";

    this.DdlStateSetings.PropertyValue = 'Id';
    this.DdlStateSetings.PropertyText = 'Name';
    this.DdlStateSetings.Method = MethodEnum.Post;
    let idCountry = this.formItem.get('IdCountry').value;
    if (idCountry > 0) {
      this.DdlStateSetings.ServiceMethodURL = urlBase + "/state/GetByCountry";
      this.DdlStateSetings.Parameter = new RequestById();
      this.DdlStateSetings.Parameter.Id = idCountry;
    }

    this.DdlCitySetings.PropertyValue = 'Id';
    this.DdlCitySetings.PropertyText = 'Name';
    this.DdlCitySetings.Method = MethodEnum.Post;
    let idState = this.formItem.get('IdState').value;
    if (idState > 0) {
      this.DdlCitySetings.ServiceMethodURL = urlBase + "/city/GetByState";
      this.DdlCitySetings.Parameter = new RequestById();
      this.DdlCitySetings.Parameter.Id = idState;
    }


    let typeList = Methods.EnumToArray(AddressType);
    this.DdlTypeSetings.DataSource = typeList;

    let areaTypeList = Methods.EnumToArray(PublicAreaType);
    this.DdlPublicAreaTypeSetings.DataSource = areaTypeList;
  }

  CepfocusOut(index) {
    // this._enderecoService.getFromPostalCode(this.formG.value.Enderecos[index].Cep.CepCod)
    //   .then(response => {           var res = response.json()
    //     if (res != null) {
    //       this.PaisChange({ idx: index, value: this.arrayPaises[index].filter(x => x.Sigla == "BR")[0] });
    //       this.formG.value.Enderecos[index].Bairro = res.bairro;
    //       this.EstadoChange({ idx: index, value: this.arrayEstados[index].filter(x => x.Sigla == res.uf)[0] });
    //       this.formG.value.Enderecos[index].Logradouro = res.endereco;
    //       this.CidadeChange({ idx: index, value: this.arrayCidades[index].filter(x => x.Nome == res.cidade)[0] });
    //     }
    //     else {
    //       this._notificationsService.error("Erro", "CEP Invalido");
    //       this.enderecos[index].Cep.CepCod = null;
    //     }
    //   });
  }

  DropDownCountryChange(obj: any) {
    if (obj.Id > 0) {
      this.DdlStateSetings.Parameter = new RequestById();
      this.DdlStateSetings.ServiceMethodURL = urlBase + "/state/GetByCountry";
      this.DdlStateSetings.Parameter.Id = obj.Id;
      this.ddlState.Reload();
    }
  }

  DropDownStateChange(obj: any) {
    if (obj.Id > 0) {
      this.DdlCitySetings.Parameter = new RequestById();
      this.DdlCitySetings.ServiceMethodURL = urlBase + "/city/GetByState";
      this.DdlCitySetings.Parameter.Id = obj.Id;
      this.ddlCity.Reload();
    }
  }

  Delete() {
    this.Remove.emit(this.formItem);
  }
}

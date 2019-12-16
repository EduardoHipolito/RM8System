import { Component, ViewEncapsulation, HostListener, EventEmitter, Input, Output, OnInit, OnChanges, ViewChildren } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { Address } from '../../_domains/Address';
import { City } from '../../_domains/City';
import { Country } from '../../_domains/Country';
import { State } from '../../_domains/State';
import { NotificationsService } from '../../_framework/notification/notifications.service';
import { AddressService } from './address.service';
import { DropDownSetings, MethodEnum } from '../../_framework/dropdown/DropDownSetings';
import { urlBase } from '../../_framework/helppers/configs';
import { RequestById } from '../../_framework/models/RequestById';
import { DropDownComponent } from '../../_framework/dropdown/dropdown.component';

@Component({
  selector: 'address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.css']
})
export class AddressComponent implements OnInit {

  @Input() public mode: string = "read";
  @Input() public addresses: Array<Address>;;
  @Output() Remove: EventEmitter<any> = new EventEmitter();

  public addressText: string;
  public country: Country;
  public state: State;
  public city: City;

  @Input() public formG: FormGroup;
  constructor(private _notificationsService: NotificationsService) {

  }

  Delete(item: FormGroup) {
    this.Remove.emit(item);
  }
  ngOnInit() {
    // let control = <FormArray>this.formG.controls['Enderecos'];
    // let j: number;
    // for (var i = 0; i < control.length;) {
    //   if (i != j) {
    //     j = i;
    //     this._pais.GetAll()
    //       .then(response => {           var res = response.json()
    //         control[i] = res;
    //         i++;
    //       })
    //       .catch(res => { i++; });
    //   }
    // }
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


}

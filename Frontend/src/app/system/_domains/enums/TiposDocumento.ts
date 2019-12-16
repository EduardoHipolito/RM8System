import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { KeyNumberValueStringObsString } from './../../_domains/types/KeyNumberValueStringObsString';
import { KeystringValueString } from './../../_domains/types/KeyStringValueString';


@Component({
    selector: 'TipoDocumento',
    template: `
  <div [formGroup]="formGr">
                <div class="col-7 col-sm-7 col-md-7 col-lg-7 col-xl-7">
    Nacionalidade 
  <select id="NacionalidadeDocumento" class="custom-select validate form-control"
  (change)="ChangeDictNac($event.target.value)">
  <option>Selecione</option>
  <option 
      *ngFor="let Tipo of DictNac"
       value="{{Tipo.Key}}">{{Tipo.Value}}</option>
</select>
  </div>
                <div class="col-5 col-sm-5 col-md-5 col-lg-5 col-xl-5">
                Tipo
<select formControlName="Tipo" id="TipoDocumento" class="custom-select validate form-control">
  <option>Selecione</option>
  <option 
      *ngFor="let Tipo of dictDoc"
       value="{{Tipo.Key}}">{{Tipo.Value}}</option>
</select>
      </div> 
      </div>    `})


export class TiposDocumento {

  @Input() public formGr: FormGroup;
    public dictDoc: Array<KeyNumberValueStringObsString> = new Array<KeyNumberValueStringObsString>();

    public Dict: Array<KeyNumberValueStringObsString> = [
        new KeyNumberValueStringObsString(1, 'CPF', 'Brasil'),
        new KeyNumberValueStringObsString(2, 'RG', 'Brasil'),
        new KeyNumberValueStringObsString(3, 'CNPJ', 'Brasil'),
        new KeyNumberValueStringObsString(4, 'IE', 'Brasil'),
        new KeyNumberValueStringObsString(5, 'IM', 'Brasil'),
        new KeyNumberValueStringObsString(6, 'IR', 'Paraguai'),
        new KeyNumberValueStringObsString(7, 'ID', 'Estados Unidos'),

    ]
    public DictNac: Array<KeystringValueString> = [
        new KeystringValueString('Brasil', 'Brasil'),
        new KeystringValueString('Paraguai', 'Paraguai'),
        new KeystringValueString('Estados Unidos', 'Estados Unidos')

    ]
ChangeDictNac(value)
{
    this.dictDoc = this.Dict.filter(x => x.Obs == value);
}
}


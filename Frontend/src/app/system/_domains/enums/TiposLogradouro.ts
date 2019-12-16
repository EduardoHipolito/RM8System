import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { KeyNumberValueString } from './../../_domains/types/KeyNumberValueString';


@Component({
selector: 'TiposLogradouro',
  template: `
  <div [formGroup]="formGr">
<select formControlName="TipoLogradouro" id="TipoLogradouro" class="custom-select validate form-control">
  <option>Selecione</option>
  <option *ngFor="let Tipo of Dict"
       value="{{Tipo.Key}}">{{Tipo.Value}}</option>
</select>
</div>
  `
})


export class TiposLogradouro
{

@Input() public formGr: FormGroup;
    public Dict: Array<KeyNumberValueString> = [
        new KeyNumberValueString(1, 'Avenida'),
        new KeyNumberValueString(2, 'Alameda'),
        new KeyNumberValueString(3, 'Condomínio'),
        new KeyNumberValueString(4, 'Estrada'),
        new KeyNumberValueString(5, 'Favela'),
        new KeyNumberValueString(6, 'Loteamento'),
        new KeyNumberValueString(7, 'Residencial'),
        new KeyNumberValueString(8, 'Rodovia'),
        new KeyNumberValueString(9, 'Rua'),
        new KeyNumberValueString(10, 'Travessa'),
        new KeyNumberValueString(11, 'Trevo'),
        new KeyNumberValueString(12, 'Via'),
        new KeyNumberValueString(13, 'Viaduto'),
        new KeyNumberValueString(14, 'Viela')
    ]

}
import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { KeyNumberValueString } from './../../_domains/types/KeyNumberValueString';


@Component({
  selector: 'TiposEndereco',
  template: `
  <div [formGroup]="formGr">
    <select formControlName="Tipo" id="Tipo" class="custom-select validate form-control">
      <option>Selecione</option>
      <option *ngFor="let Tipo of Dict" value="{{Tipo.Key}}">{{Tipo.Value}}</option>
    </select>
  </div>
  `
})


export class TiposEndereco {

  @Input() public formGr: FormGroup;

  public Dict: Array<KeyNumberValueString> = [
    new KeyNumberValueString(1, 'Residencial'),
    new KeyNumberValueString(2, 'Comercial'),
    new KeyNumberValueString(3, 'Entrega'),
    new KeyNumberValueString(4, 'Correspondencia'),
  ]

}
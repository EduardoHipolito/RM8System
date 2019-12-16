import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


import { KeystringValueString } from './../../_domains/types/KeyStringValueString';


@Component({
    selector: 'TipoTelefone',
    template: `
  <div [formGroup]="formGr">
  Tipo Telefone
<select formControlName="Tipo" id="TipoTelefone" class="custom-select validate form-control">
  <option>Selecione</option>
  <option 
      *ngFor="let Tipo of Dict"
       value="{{Tipo.Key}}">{{Tipo.Value}}</option>
</select>
</div>
  `
})


export class TiposTelefone { 

  @Input() public formGr: FormGroup;
    public Dict: Array<KeystringValueString> = [
        new KeystringValueString('R', 'Residencial'),
        new KeystringValueString('CM', 'Comercial'),
        new KeystringValueString('CL', 'Celular')
    ]

}


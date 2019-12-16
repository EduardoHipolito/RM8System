import { Component } from '@angular/core';

import { KeystringValueString } from './../../_domains/types/KeyStringValueString';


@Component({
  selector: 'TipoStatus',
  template: `
<select id="TipoStatus" class="custom-select validate form-control">
  <option>Selecione</option>
  <option 
      *ngFor="let Tipo of Dict"
       value="{{Tipo.Key}}">{{Tipo.Value}}</option>
</select>
  `
})


export class TiposStatus {

    public Dict: Array<KeystringValueString> = [
        new KeystringValueString("A", 'Ativo'),
        new KeystringValueString("I", 'Inativo')
    ]

}


import { Component, ViewEncapsulation, HostListener, EventEmitter, Input, Output,OnInit , OnChanges} from '@angular/core';
import { NgClass } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Phone } from '../../_domains/Phone';
import { baseComponent } from '../../_framework/helppers/base.component';
import { DropDownSetings, MethodEnum } from '../../_framework/dropdown/DropDownSetings';
import { PhoneType } from '../../_domains/enums/PhoneType';
import { urlBase } from '../../_framework/helppers/configs';
import { Methods } from '../../_framework/helppers/methods';

@Component({
  selector: 'phone',
  templateUrl: './phone.component.html',
  styleUrls: ['./phone.component.css']
})
export class PhoneComponent extends baseComponent implements OnInit {

  @Input() public formG: FormGroup;
  @Input() public mode:string = "read";
  @Input() public phones: Array<Phone>;;
  @Output() Remove: EventEmitter<any> = new EventEmitter();
  DdlTypesSetings: DropDownSetings = new DropDownSetings();
  DdlCountrySetings: DropDownSetings = new DropDownSetings();
 
  constructor() { 
    super();
  }

  ngOnInit() {
    let typeList = Methods.EnumToArray(PhoneType);
    this.DdlTypesSetings.DataSource = typeList;
    this.DdlCountrySetings.PropertyValue = 'Id';
    this.DdlCountrySetings.PropertyText = 'Name';
    this.DdlCountrySetings.Method = MethodEnum.Get;
    this.DdlCountrySetings.ServiceMethodURL = urlBase + "/country/GetAll";
  }
  Delete(phone) {
    this.Remove.emit(phone);
  }
}

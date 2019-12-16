import { Component, ViewEncapsulation, HostListener, EventEmitter, Input, Output, OnInit, OnChanges } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ProductEntry } from '../../_domains/ProductEntry';
import { baseComponent } from '../../_framework/helppers/base.component';
import { DropDownSetings, MethodEnum } from '../../_framework/dropdown/DropDownSetings';
import { urlStock } from '../../_framework/helppers/configs';
import { Methods } from '../../_framework/helppers/methods';

@Component({
  selector: 'productentry',
  templateUrl: './productentry.component.html',
  styleUrls: ['./productentry.component.css']
})
export class ProductEntryComponent extends baseComponent implements OnInit {

  @Output() onChange: EventEmitter<any> = new EventEmitter();
  @Input() public formG: FormGroup;
  @Input() public mode: string = "read";
  @Input() public ProductEntries: Array<ProductEntry>;;
  DdlProductSetings: DropDownSetings = new DropDownSetings();

  constructor() {
    super();
  }

  changeTotalValue(val, form) {
    var total = (this.getValueFromForm(form, 'ICMS') + this.getValueFromForm(form, 'IPI') + (this.getValueFromForm(form, 'UnitPrice') * this.getValueFromForm(form, 'Quantity')));
    form.controls['TotalPrice'].setValue(total);
    this.onChange.next();
  }

  getValueFromForm(form, propertyName) {
    return Methods.stringToNumber(form.get(propertyName).value);
  }

  ngOnInit() {
    this.DdlProductSetings.PropertyValue = 'Id';
    this.DdlProductSetings.PropertyText = 'Name';
    this.DdlProductSetings.Method = MethodEnum.Get;
    this.DdlProductSetings.ServiceMethodURL = urlStock + "/product/GetAll";
  }
}

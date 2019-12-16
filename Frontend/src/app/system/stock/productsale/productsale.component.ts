import { Component, ViewEncapsulation, HostListener, EventEmitter, Input, Output, OnInit, OnChanges } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ProductSale } from '../../_domains/ProductSale';
import { baseComponent } from '../../_framework/helppers/base.component';
import { DropDownSetings, MethodEnum } from '../../_framework/dropdown/DropDownSetings';
import { PhoneType } from '../../_domains/enums/PhoneType';
import { urlStock } from '../../_framework/helppers/configs';
import { Methods } from '../../_framework/helppers/methods';

@Component({
  selector: 'productsale',
  templateUrl: './productsale.component.html',
  styleUrls: ['./productsale.component.css']
})
export class ProductSaleComponent extends baseComponent implements OnInit {

  @Input() public formG: FormGroup;
  @Input() public productSale: Array<ProductSale>;
  DdlProductSetings: DropDownSetings = new DropDownSetings();

  constructor() {
    super();
  }

  ngOnInit() {
    this.DdlProductSetings.PropertyValue = 'Id';
    this.DdlProductSetings.PropertyText = 'Name';
    this.DdlProductSetings.Method = MethodEnum.Get;
    this.DdlProductSetings.ServiceMethodURL = urlStock + "/product/GetAll";
  }

  DropDownProductChange(obj: any, form) {
    form.controls['FKProduct'].setValue(obj);
  }
}

import { Component, ViewEncapsulation, HostListener, EventEmitter, Input, Output,OnInit , OnChanges} from '@angular/core';
import { NgClass } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Phone } from '../../_domains/Phone';
import { baseComponent } from '../../_framework/helppers/base.component';
import { DropDownSetings, MethodEnum } from '../../_framework/dropdown/DropDownSetings';
import { PhoneType } from '../../_domains/enums/PhoneType';
import { urlStock } from '../../_framework/helppers/configs';
import { Methods } from '../../_framework/helppers/methods';
import { Payment } from '../../_domains/Payment';

@Component({
  selector: 'payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent extends baseComponent implements OnInit {

  @Input() public formG: FormGroup;
  @Input() public mode:string = "read";
  @Input() public payments: Array<Payment>;;
  DdlFormOfPaymentSetings: DropDownSetings = new DropDownSetings();
 
  constructor() { 
    super();
  }

  ngOnInit() {
    this.DdlFormOfPaymentSetings.PropertyValue = 'Id';
    this.DdlFormOfPaymentSetings.PropertyText = 'Name';
    this.DdlFormOfPaymentSetings.Method = MethodEnum.Get;
    this.DdlFormOfPaymentSetings.ServiceMethodURL = urlStock + "/formofpayment/GetAll";
  }
}

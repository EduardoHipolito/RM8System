import { Component, ViewEncapsulation, HostListener, EventEmitter, Input, Output, OnInit, OnChanges } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Document } from '../../_domains/Document';
import { baseComponent } from '../../_framework/helppers/base.component';
import { DropDownSetings } from '../../_framework/dropdown/DropDownSetings';
import { MethodEnum } from '../../_framework/helppers/methods';
import { urlBase } from '../../_framework/helppers/configs';

@Component({
  selector: 'document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.css']
})
export class DocumentComponent extends baseComponent implements OnInit {

  @Input() public formG: FormGroup;
  @Input() public mode: string = "read";
  @Input() public documents: Array<Document>;
  @Output() Remove: EventEmitter<any> = new EventEmitter();

  DdlTypesSetings: DropDownSetings = new DropDownSetings();

  constructor() { 
    super();
  }

  ngOnInit(): void {    
    this.DdlTypesSetings.PropertyValue = 'Id';
    this.DdlTypesSetings.PropertyText = 'Name';
    this.DdlTypesSetings.Method = MethodEnum.Get;
    this.DdlTypesSetings.ServiceMethodURL = urlBase + "/documenttype/GetAll";
  }
  Delete(doc) {
    this.Remove.emit(doc);
  }
}

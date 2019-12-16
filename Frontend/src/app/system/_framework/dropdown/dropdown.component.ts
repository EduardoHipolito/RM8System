import { Component, ViewEncapsulation, HostListener, EventEmitter, Input, Output, AfterViewInit } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { DropDownSetings, MethodEnum } from './DropDownSetings';
import { DropDownService } from './dropdown.service';

@Component({
    selector: 'DropDown',
    templateUrl: './dropdown.html',
    styleUrls: ['./dropdown.css']
})
export class DropDownComponent implements AfterViewInit {

    @Input() setings: DropDownSetings = new DropDownSetings();
    @Input() form: FormGroup = new FormGroup({});
    @Input() label: string = "";
    @Input() controlName: string = "";
    @Output() SelectChange: EventEmitter<any> = new EventEmitter();


    constructor(private dropDownService: DropDownService) {

    }

    getObject(Id) {
        let p = this.setings.DataSource.find(f => f[this.setings.PropertyValue] == Id);
        if (p != null) {
            this.SelectChange.emit(p);
        }
    }

    public Reload() {
        this.load();
    }

    load() {

        if (this.setings.ServiceMethodURL != null && this.setings.Method != null) {
            switch (this.setings.Method) {
                case MethodEnum.Get:
                    {
                        let context = this;
                        this.dropDownService.Get(this.setings.ServiceMethodURL)
                            .then(
                                response => {
                                    let res = response.json();
                                    context.setings.DataSource = res.Data;
                                },
                                error => console.log(error)
                            );
                        break;
                    }
                case MethodEnum.Post:
                    {
                        let context = this;
                        this.dropDownService.Post(this.setings.ServiceMethodURL, this.setings.Parameter)
                            .then(
                                response => {
                                    let res = response.json();
                                    context.setings.DataSource = res.Data;
                                },
                                error => console.log(error)
                            );
                        break;
                    }
            }
        }
    }
    ngAfterViewInit(): void {
        this.load();
        let context = this;
        this.form.get(this.controlName).valueChanges.subscribe(value => {
            this.getObject(context);
        });
    }

    public getData(row: any, propertyName: string): string {
        if (propertyName.indexOf('?') > 0) {
            var question1 = propertyName.split('?');
            var question2 = question1[1].split(':');
            if (row[question1[0]] != null) {
                return question2[0].split('.').reduce((prev: any, curr: string) => prev[curr], row);
            } else {
                return question2[1];
            }
        } else {
            return propertyName.split('.').reduce((prev: any, curr: string) => prev[curr], row);
        }
    }
}
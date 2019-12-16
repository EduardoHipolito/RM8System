import { Component, ViewEncapsulation, HostListener, EventEmitter, Input, Output, AfterViewInit } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Methods } from '../helppers/methods';

@Component({
    selector: 'TextBox',
    templateUrl: './textbox.html',
    styleUrls: ['./textbox.css']
})
export class TextBoxComponent implements AfterViewInit {

    @Input() form: FormGroup = new FormGroup({});
    @Input() controlName: string = "";
    @Input() label: string = "";
    @Input() readonly: boolean = false;
    @Input() type: string = "text";


    @Input() value = '';
    @Input() minimumDate: boolean = false;
    @Input() onlyDatePicker: boolean = false;

    @Output() onChange: EventEmitter<any> = new EventEmitter();

    private internalModel: any;

    ngAfterViewInit() {
        this.internalModel = Methods.CloneObject(this.form.get(this.controlName).value);
    }

    Change(): void {
        let context = this;

        var value = this.form.get(this.controlName).value;
        
        // value = value.toString().trim();
        // this.form.controls[this.controlName].setValue(value);

        this.onChange.emit(value);
    }

    isNumber(e) { return typeof e === 'number' }

    getModelFromControlName() {
        return this.form.get(this.controlName);
    }

    currencyInputChanged(value: string) {
        if(this.type == 'currency')
        {
            return Methods.stringToNumber(value)
        }
        return value;
    }



    public get model() {
        return this.internalModel;
    }

    public set model(value) {
        this.internalModel = value;
        // if (this.type == 'currency') {
        //     value = value.replace('R$', '');
        // }
        this.form.controls[this.controlName].setValue(value);
    }

    get isInvalid(){
       return this.form.get(this.controlName).invalid
    }
}
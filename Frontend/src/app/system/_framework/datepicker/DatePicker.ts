import { Input, Output, EventEmitter, ElementRef, Directive, AfterViewInit, SimpleChanges, HostListener, Renderer2, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, FormGroup } from '@angular/forms';
declare var jQuery: any;
export const SPLITTER_VALUE_ACCESSOR =
  { provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => DatePicker), multi: true, };

@Directive({
  selector: '[myDatepicker]',
  providers: [SPLITTER_VALUE_ACCESSOR]
})

export class DatePicker implements ControlValueAccessor, AfterViewInit {
  _isDisabled: boolean = false;
  writeValue(value) {
    this.value = value;
  }
  registerOnChange(fn: any): void {
    // throw new Error("Method not implemented.");
  }
  registerOnTouched(fn: any): void {
    // throw new Error("Method not implemented.");
  }
  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.renderer.setAttribute(this.el.nativeElement, 'readonly', 'true');
      this._isDisabled = true;

    } else {
      this._isDisabled = false;
      this.renderer.removeAttribute(this.el.nativeElement, 'readonly');
    }
  }

  @Input() value = '';
  @Input() controlName = '';
  @Input() form: FormGroup;
  @Input() minimumDate: boolean = false;
  @Input() onlyDatePicker: boolean = false;
  @Output() dateChange = new EventEmitter();

  constructor(public el: ElementRef, public renderer: Renderer2) { }

  ngAfterViewInit() {
    if (this.el.nativeElement.readOnly === false) {

      if (this.onlyDatePicker === true) {
        jQuery(this.el.nativeElement).datepicker({
          controlType: 'select',
          dateFormat: 'dd/mm/yy',
          dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
          dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
          dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
          monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
          monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
          nextText: 'Próximo',
          prevText: 'Anterior',
          timeText: 'Hora',
          hourText: 'Hora',
          minuteText: 'Minuto',
          secondText: 'Segundo',
          currentText: 'Agora',
          closeText: 'Fechar',
          oneLine: true,
          onSelect: (value) => {
            this.value = value;
            this.dateChange.next(value);
          }
        });
          //.datepicker('setDate', this.value);
      }
      else {
        jQuery(this.el.nativeElement).datetimepicker({
          controlType: 'select',
          oneLine: true,
          showSecond: true,
          dateFormat: 'dd/mm/yy',
          timeFormat: 'HH:mm:ss',
          dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
          dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
          dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
          monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
          monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
          nextText: 'Próximo',
          prevText: 'Anterior',
          timeText: 'Hora',
          hourText: 'Hora',
          minuteText: 'Minuto',
          secondText: 'Segundo',
          currentText: 'Agora',
          closeText: 'Fechar',
          onSelect: (value) => {
            this.value = value;
            this.dateChange.next(value);
          }
        });
         // .datepicker('setDate', this.value);
      }
    }
    let context = this;
    this.form.valueChanges.subscribe(data => {
      if (data[context.controlName] != null) {
        context.value = new Date(data[context.controlName]).toLocaleString("pt-BR");
        if (this.onlyDatePicker === true) {
          context.el.nativeElement.value = context.value;
        }else{
          jQuery(context.el.nativeElement).datepicker('setDate', new Date(data[context.controlName]));
          jQuery(context.el.nativeElement).datepicker('setTime', new Date(data[context.controlName]));
        }
      }
    });
  }
}
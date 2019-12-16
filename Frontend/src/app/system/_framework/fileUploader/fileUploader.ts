import { Component, OnInit, Input, AfterViewInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { encode_utf8, decode_utf8 } from '../helppers/stringToByteArray'

@Component({
    selector: 'fileUploader',
    templateUrl: './fileUploader.html',
    styleUrls: ['./fileUploader.css']
})
export class fileUploader implements AfterViewInit {

    ngAfterViewInit(): void {
        this.form.controls[this.controlName].valueChanges.subscribe(data => {
            this.imageSrc = data;
        });
    }

    @Input() form: FormGroup = new FormGroup({});
    @Input() controlName: string = "";
    @Input() label: string = "";

    activeColor: string = 'green';
    baseColor: string = '#ccc';
    overlayColor: string = 'rgba(255,255,255,0.5)';

    dragging: boolean = false;
    loaded: boolean = false;
    imageLoaded: boolean = false;

    imageSrc: string = '';

    handleDragEnter() {
        this.dragging = true;
    }

    handleDragLeave() {
        this.dragging = false;
    }

    handleDrop(e) {
        e.preventDefault();
        this.dragging = false;
        this.handleInputChange(e);
    }

    handleImageLoad() {
        this.imageLoaded = true;
    }

    handleInputChange(e) {
        var file = e.dataTransfer ? e.dataTransfer.files[0] : e.target.files[0];

        var pattern = /image-*/;
        var reader = new FileReader();

        if (!file.type.match(pattern)) {
            alert('invalid format');
            return;
        }
        if(file.size > 2e+6){
            alert('tamanho não suportado');
            return;
        }

        this.loaded = false;

        reader.onload = this._handleReaderLoaded.bind(this);
        reader.readAsDataURL(file);
    }

    _handleReaderLoaded(e) {
        var reader = e.target;
        this.imageSrc = reader.result;
        this.loaded = true;
        this.form.controls[this.controlName].setValue((reader.result));
    }
}
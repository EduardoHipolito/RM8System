import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { SystemRouting } from './system.routing';
import { SystemComponent } from './system.component';

import { StockComponent } from './stock/stock.component';
import { BaseComponent } from './base/base.component';
import { FrameworkModule } from './_framework/framework.module';
import { HttpModule, XHRBackend } from '@angular/http';
import { ExtendedXHRBackend } from './_framework/http/extendedXHRBackend';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    FrameworkModule,
    SystemRouting
  ],
  declarations: [
    BaseComponent,
    StockComponent
  ],
  providers: [
    { provide: XHRBackend, useClass: ExtendedXHRBackend },
  ],
  exports: [
  ]
})

export class SystemModule { }

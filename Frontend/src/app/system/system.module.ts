import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { SystemRouting } from './system.routing';
import { SystemComponent } from './system.component';

import { StockComponent } from './stock/stock.component';
import { BaseComponent } from './base/base.component';
import { FrameworkModule } from './_framework/framework.module';
import { ExtendedHttpInterceptor } from './_framework/http/extendedHttpInterceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    FrameworkModule,
    SystemRouting
  ],
  declarations: [
    BaseComponent,
    StockComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ExtendedHttpInterceptor, multi: true },
    // { provide: XHRBackend, useClass: ExtendedXHRBackend },
  ],
  exports: [
  ]
})

export class SystemModule { }

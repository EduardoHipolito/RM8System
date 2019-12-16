import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, JsonpModule, XHRBackend, Http } from '@angular/http';
import { CommonModule, registerLocaleData, CurrencyPipe } from '@angular/common';
import localePt from '@angular/common/locales/pt';

import { LoginComponent } from './system/login/login.component'

import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';

import { SystemModule } from './system/system.module';
import { SystemComponent } from './system/system.component';
import { ExtendedXHRBackend } from './system/_framework/http/extendedXHRBackend';
import { FrameworkModule } from './system/_framework/framework.module';

registerLocaleData(localePt, 'pt-BR');

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SystemComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    HttpModule,
    FrameworkModule.forRoot(),
    AppRoutingModule
  ],
  providers: [
    { provide: XHRBackend, useClass: ExtendedXHRBackend },
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    CurrencyPipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { BaseRouting } from './base.routing'
import { TiposDocumento } from '../_domains/enums/TiposDocumento';
import { TiposStatus } from '../_domains/enums/TiposStatus';
import { TiposTelefone } from '../_domains/enums/TiposTelefone';
import { TiposEndereco } from '../_domains/enums/TiposEndereco';
import { TiposLogradouro } from '../_domains/enums/TiposLogradouro';
import { FrameworkModule } from '../_framework/framework.module';
import { HttpModule, XHRBackend } from '@angular/http';
import { CountryComponent } from './country/country.component';
import { StateComponent } from './state/state.component';
import { CityComponent } from './city/city.component';
import { ModuleComponent } from './module/module.component';
import { UserComponent } from './user/user.component';
import { AplicationComponent } from './aplication/aplication.component';
import { CnaeComponent } from './cnae/cnae.component';
import { CompanyComponent } from './company/company.component';
import { UserAplicationCompanyComponent } from './useraplicationcompany/useraplicationcompany.component';
import { LegalPersonComponent } from './legalperson/legalperson.component';
import { PhysicalPersonComponent } from './physicalperson/physicalperson.component';
import { AddressComponent } from './address/address.component';
import { AddressItemComponent } from './address/addressitem/address.item.component';
import { DocumentComponent } from './document/document.component';
import { PhoneComponent } from './phone/phone.component';
import { AddressService } from './address/address.service';
import { ExtendedXHRBackend } from '../_framework/http/extendedXHRBackend';
import { HomeService } from './home/home.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    FrameworkModule,
    BaseRouting
  ],
  declarations: [
    LegalPersonComponent,
    PhysicalPersonComponent,
    HomeComponent,
    AddressComponent,
    AddressItemComponent,
    TiposDocumento,
    TiposStatus,
    TiposTelefone,
    TiposEndereco,
    TiposLogradouro,
    DocumentComponent,
    PhoneComponent,
    CountryComponent,
    StateComponent,
    CityComponent,
    UserComponent,
    ModuleComponent,
    CnaeComponent,
    UserAplicationCompanyComponent,
    CompanyComponent,
    AplicationComponent
  ],
  providers: [
    { provide: XHRBackend, useClass: ExtendedXHRBackend },
    AddressService,
    HomeService
  ],
  exports: [
    LegalPersonComponent,
    PhysicalPersonComponent,
    HomeComponent,
    AddressComponent,
    AddressItemComponent,
    TiposDocumento,
    TiposStatus,
    TiposTelefone,
    TiposEndereco,
    TiposLogradouro,
    DocumentComponent,
    PhoneComponent,
    CountryComponent,
    StateComponent,
    CityComponent,
    AplicationComponent,
    CnaeComponent,
    UserAplicationCompanyComponent,
    CompanyComponent,
    ModuleComponent
  ]
})

export class BaseModule { }

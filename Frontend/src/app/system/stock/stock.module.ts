import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { StockRouting } from './stock.routing'
import { FrameworkModule } from '../_framework/framework.module';
import { HttpModule, XHRBackend } from '@angular/http';
import { CategoryComponent } from './category/category.component'
import { SupplierComponent } from './supplier/supplier.component';
import { ProductComponent } from './product/product.component';
import { EntryComponent } from './entry/entry.component';
import { FormOfPaymentComponent } from './formofpayment/formofpayment.component';
import { CustomerComponent } from './customer/customer.component';
import { SaleComponent } from './sale/sale.component';
import { StockSummaryComponent } from './stocksummary/stocksummary.component';
import { StockSummaryService } from './stocksummary/stocksummary.service';
import { SaleService } from './sale/sale.service';
import { ProductEntryComponent } from './productentry/productentry.component';
import { ProductSaleComponent } from './productsale/productsale.component';
import { PaymentComponent } from './payment/payment.component';
import { ExtendedXHRBackend } from '../_framework/http/extendedXHRBackend';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    FrameworkModule,
    StockRouting

  ],
  declarations: [
    CategoryComponent,
    SupplierComponent,
    ProductComponent,
    EntryComponent,
    ProductEntryComponent,
    CustomerComponent,
    FormOfPaymentComponent,
    StockSummaryComponent,
    SaleComponent,
    ProductSaleComponent,
    PaymentComponent
  ],
  providers: [
    { provide: XHRBackend, useClass: ExtendedXHRBackend },
    StockSummaryService,
    SaleService
  ],
  exports: [
    CategoryComponent,
    SupplierComponent,
    ProductComponent,
    EntryComponent,
    ProductEntryComponent,
    CustomerComponent,
    FormOfPaymentComponent,
    StockSummaryComponent,
    SaleComponent,
    ProductSaleComponent,
    PaymentComponent
  ]
})

export class StockModule { }

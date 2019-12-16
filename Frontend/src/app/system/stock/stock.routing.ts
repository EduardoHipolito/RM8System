import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { CategoryComponent } from './category/category.component';
import { ProductComponent } from './product/product.component';
import { EntryComponent } from './entry/entry.component';
import { SupplierComponent } from './supplier/supplier.component';
import { FormOfPaymentComponent } from './formofpayment/formofpayment.component';
import { CustomerComponent } from './customer/customer.component';
import { StockSummaryComponent } from './stocksummary/stocksummary.component';
import { SaleComponent } from './sale/sale.component';
import { AuthGuard } from '../_framework/auth/auth.guard';

const stockRoutes = [
    { path: 'stocksummary', component: StockSummaryComponent, canActivate: [AuthGuard]  },
    { path: 'category', component: CategoryComponent, canActivate: [AuthGuard]  },
    { path: 'supplier', component: SupplierComponent, canActivate: [AuthGuard]  },
    { path: 'product', component: ProductComponent, canActivate: [AuthGuard]  },
    { path: 'entry', component: EntryComponent, canActivate: [AuthGuard]  },
    { path: 'formofpayment', component: FormOfPaymentComponent, canActivate: [AuthGuard]  },
    { path: 'customer', component: CustomerComponent, canActivate: [AuthGuard]  },
    { path: 'sale', component: SaleComponent, canActivate: [AuthGuard]  },
];

@NgModule({
    imports: [RouterModule.forChild(stockRoutes)],
    exports: [RouterModule]
})
export class StockRouting { }
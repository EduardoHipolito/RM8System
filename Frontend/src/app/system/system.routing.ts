import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BaseComponent } from './base/base.component';
import { StockComponent } from './stock/stock.component';

const systemRoutes = [
    {
        path: 'base', component: BaseComponent, loadChildren: './base/base.module#BaseModule'
    },
    {
        path: 'stock', component: StockComponent, loadChildren: './stock/stock.module#StockModule'
    },
    { path: '', redirectTo: '/system/base', pathMatch: "full" }
];

@NgModule({
    imports: [RouterModule.forChild(systemRoutes)],
    exports: [RouterModule]
})
export class SystemRouting { }
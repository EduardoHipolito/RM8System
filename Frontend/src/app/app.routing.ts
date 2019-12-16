import { NgModule } from '@angular/core';
import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './system/login/login.component';
import { AuthLoginGuard } from './system/_framework/auth/auth.login.guard';
import { SystemComponent } from './system/system.component';
import { SystemModule } from './system/system.module';

// const appRoutes: Routes = [
//     { path: 'login',pathMatch: 'full', component: LoginComponent },
//     { path: 'pessoa', pathMatch: 'full', component: PessoaComponent, canActivate: [AuthGuard] },
//     { path: 'home', pathMatch: 'full', component: HomeComponent, canActivate: [AuthGuard] },
//     { path: '', pathMatch: 'full', component: HomeComponent, canActivate: [AuthGuard] },
//     { path: '**', redirectTo: 'login' }
// ];

const appRoutes: Routes = [
    { path: '', redirectTo: 'system', pathMatch: 'full' },
    { path: 'system', component: SystemComponent, loadChildren: './system/system.module#SystemModule' },
    { path: 'login', component: LoginComponent, canActivate: [AuthLoginGuard] },
    { path: '**', redirectTo: 'system' }
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes, { useHash: true, enableTracing: false })],
    exports: [RouterModule]
})

export class AppRoutingModule { }

//const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
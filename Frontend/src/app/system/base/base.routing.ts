import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { HomeComponent } from './home/home.component';
import { PhysicalPersonComponent } from './physicalperson/physicalperson.component';
import { AuthGuard } from '../_framework/auth/auth.guard';
import { CountryComponent } from './country/country.component';
import { StateComponent } from './state/state.component';
import { CityComponent } from './city/city.component';
import { UserComponent } from './user/user.component';
import { ModuleComponent } from './module/module.component';
import { AplicationComponent } from './aplication/aplication.component';
import { CompanyComponent } from './company/company.component';
import { CnaeComponent } from './cnae/cnae.component';
import { UserAplicationCompanyComponent } from './useraplicationcompany/useraplicationcompany.component';
import { LegalPersonComponent } from './legalperson/legalperson.component';

const baseRoutes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard]  },
    { path: 'physicalperson', component: PhysicalPersonComponent, canActivate: [AuthGuard] },
    { path: 'legalperson', component: LegalPersonComponent, canActivate: [AuthGuard] },
    { path: 'country', component: CountryComponent, canActivate: [AuthGuard] },
    { path: 'state', component: StateComponent, canActivate: [AuthGuard] },
    { path: 'user', component: UserComponent, canActivate: [AuthGuard] },
    { path: 'useraplicationcompany', component: UserAplicationCompanyComponent, canActivate: [AuthGuard] },
    { path: 'module', component: ModuleComponent, canActivate: [AuthGuard] },
    { path: 'aplication', component: AplicationComponent, canActivate: [AuthGuard] },
    { path: 'city', component: CityComponent, canActivate: [AuthGuard] },
    { path: 'company', component: CompanyComponent, canActivate: [AuthGuard] },
    { path: 'cnae', component: CnaeComponent, canActivate: [AuthGuard] },
    { path: 'home', component: HomeComponent, canActivate: [AuthGuard] }
];

@NgModule({
    imports: [RouterModule.forChild(baseRoutes)],
    exports: [RouterModule]
})
export class BaseRouting { }
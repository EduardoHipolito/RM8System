import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { ModuleWithProviders, NgModule } from "@angular/core";

import { Sidebar } from "./sidebar/sidebar.component";
import { PageTopComponent } from "./sidebar/components/page-top/page-top.component";
import { Menu } from "./sidebar/components/menu/menu.component";
import { SelectCompanyComponent } from "./sidebar/components/select-company/select-company.component";
import { SubMenu } from "./sidebar/components/submenu/submenu.component";
import { MenuItem } from "./sidebar/components/menuItem/menuItem.component";
import { SubMenuItem } from "./sidebar/components/subMeuItem/subMenuItem.component";
import { SidebarService } from "./sidebar/sidebar.service";
import { SelectCompanyService } from "./sidebar/components/select-company/select-company.service";
import { PageLoaderService } from "./pageLoader/page-loader.service";
import { AuthService } from "./auth/auth.service";
import { AuthGuard } from "./auth/auth.guard";
import { AuthLoginGuard } from "./auth/auth.login.guard";
import { AuthCookie } from "./auth/auth.cookie";
import { AuthProps } from "./auth/auth.Props";
import { CrudplugComponent } from "./crudplug/crudplug.component";
import { GridComponent } from "./grid/grid.component";
import { CrudplugService } from "./crudplug/crudplug.service";
import { GridSortingDirective } from "./grid/grid-sorting.directive";
import { GridPagingDirective } from "./grid/grid-paging.directive";
import { GridFilteringDirective } from "./grid/grid-filtering.directive";
import { NotificationComponent } from "./notification/notification.component";
import { MaxPipe } from "./notification/max.pipe";
import { NotificationsService } from "./notification/notifications.service";
import { SimpleNotificationsComponent } from "./notification/notifications.component";
import { DatePicker } from "./datepicker/DatePicker";
import { TwoDigitDecimaNumberDirective } from "./directives/TwoDigitDecimaNumberDirective ";
import { DropDownService } from "./dropdown/dropdown.service";
import { DropDownComponent } from "./dropdown/dropdown.component";
import { fileUploader } from './fileUploader/fileUploader';
import { TextBoxComponent } from './textbox/textbox.component';
import { GridPaginationComponent } from "./grid/gridpagination/gridpagination.component";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { ExtendedHttpInterceptor } from "./http/extendedHttpInterceptor";


@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule

  ],
  declarations: [
    GridPaginationComponent,
    Sidebar,
    PageTopComponent,
    Menu,
    SubMenu,
    MenuItem,
    SubMenuItem,
    SelectCompanyComponent,
    CrudplugComponent,
    fileUploader,
    TextBoxComponent,
    GridComponent,
    GridFilteringDirective,
    GridPagingDirective,
    GridSortingDirective,
    NotificationComponent,
    SimpleNotificationsComponent,
    DatePicker,
    TwoDigitDecimaNumberDirective,
    MaxPipe,
    DropDownComponent
  ],

  exports: [
    GridPaginationComponent,
    Sidebar,
    PageTopComponent,
    Menu,
    SubMenu,
    MenuItem,
    SubMenuItem,
    SelectCompanyComponent,
    CrudplugComponent,
    GridComponent,
    TextBoxComponent,
    fileUploader,
    GridFilteringDirective,
    GridPagingDirective,
    GridSortingDirective,
    NotificationComponent,
    SimpleNotificationsComponent,
    DatePicker,
    TwoDigitDecimaNumberDirective,
    DropDownComponent
  ], providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ExtendedHttpInterceptor, multi: true },
    // { provide: XHRBackend, useClass: ExtendedXHRBackend },
    SelectCompanyService,
    SidebarService,
    DropDownService
  ]
})
export class FrameworkModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: FrameworkModule,
      providers: [
        PageLoaderService,
        AuthService,
        AuthGuard,
        AuthLoginGuard,
        AuthCookie,
        AuthProps,
        CrudplugService,
        NotificationsService
      ],
    };
  }
}

import { Input, Output, Component, OnInit, EventEmitter } from '@angular/core';

import { SelectCompanyService } from './../select-company/select-company.service';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'PageTop',
  templateUrl: './page-top.component.html',
  styleUrls: ['./page-top.component.css']
})
export class PageTopComponent {

  @Output() ClickEvent = new EventEmitter();
  public isScrolled: boolean = false;
  constructor(
    private selectCompanyService: SelectCompanyService,
    private authService: AuthService) { }

  public scrolledChanged(isScrolled) {
    this.isScrolled = isScrolled;
  }

  public LogOff() {
    this.authService.logout();
  }
  public SaveConfigurations() {
    this.selectCompanyService.setLojaPadrao($('#SelectCompany').find(":selected").val());
    location.reload();
  }
}

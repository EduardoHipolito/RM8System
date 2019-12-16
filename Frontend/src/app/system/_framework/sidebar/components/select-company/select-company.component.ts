import { Component, OnInit } from '@angular/core';
import { SelectCompanyService } from './select-company.service';
import { Company } from '../../../../_domains/Company';
import { AuthCookie } from '../../../auth/auth.cookie';

@Component({
  selector: 'SelectCompany',
  templateUrl: './select-company.component.html',
  styleUrls: ['./select-company.component.css']
})
export class SelectCompanyComponent implements OnInit {

  constructor(private _service: SelectCompanyService, private _coockie: AuthCookie) { }

  public companies: Array<Company>;
  public DefaultCompany: string;

  public ngOnInit(): void {
    this._service.updateCompanies()
      .then(response => {
        var res = response.json()
        this.companies = res.Data;
        this.getCompanyCoockie()
      });
  }

  public getCompanyCoockie() {
    this.DefaultCompany = this._coockie.GetIntoLocalStorage('IdCompany');
    if (this.DefaultCompany == null || this.DefaultCompany == "") {
      this.DefaultCompany = this.companies[0].Id.toString();
      this._coockie.SetIntoLocalStorage('IdCompany', this.DefaultCompany);
    }
  }

}

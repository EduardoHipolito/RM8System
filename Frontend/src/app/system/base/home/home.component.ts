import { Component, OnInit, AfterViewInit } from '@angular/core';
import { NotificationsService } from '../../_framework/notification/notifications.service';
import { AuthService } from '../../_framework/auth/auth.service';
import { HomeService } from './home.service';
import { SumByQuantity } from '../../_domains/SumByQuantity';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements AfterViewInit {

  _day:number = 0;
  _dayText:string = "";
  _month:number = 0;
  _monthText:string = "";
  _date:any = Date.now();
  _dailySales: boolean = false;
  _dailyEntries: boolean = false;
  _dailyProfit: boolean = false;
  _monthSales: boolean = false;
  _monthEntries: boolean = false;
  _monthProfit: boolean = false;
  _dailySalesValue: SumByQuantity = new SumByQuantity();
  _dailyEntriesValue: SumByQuantity = new SumByQuantity();
  _dailyProfitValue: SumByQuantity = new SumByQuantity();
  _monthSalesValue: SumByQuantity = new SumByQuantity(); 
  _monthEntriesValue: SumByQuantity = new SumByQuantity();
  _monthProfitValue: SumByQuantity = new SumByQuantity();

  constructor(private notificationsService: NotificationsService, private homeService: HomeService, private authService: AuthService) {
  }

  ngAfterViewInit() {
    var context = this;
    setTimeout(function () { context.UpdatePermitions(); context.GetValues(); }, 5000);
    setInterval(function () { context.UpdatePermitions(); context.GetValues(); }, 300000);
  }
  UpdatePermitions() {
    this._dailySales = this.authService.hasPermition('DAILYSALESUMMARY');
    this._dailyEntries = this.authService.hasPermition('DAILYENTRYSUMMARY');
    this._dailyProfit = this.authService.hasPermition('DAILYPROFITSUMMARY');
    this._monthSales = this.authService.hasPermition('MONTHSALESUMMARY');
    this._monthEntries = this.authService.hasPermition('MONTHENTRYSUMMARY');
    this._monthProfit = this.authService.hasPermition('MONTHPROFITSUMMARY');
  }
  GetValues() {
    if (this._dailySales) {
      this.homeService.GetDailySalesAmout()
        .then(response => {
          var res = response.json();
          this._dailySalesValue = res.Data;
        });
    }
    if (this._monthSales) {
      this.homeService.GetMonthSalesAmout()
        .then(response => {
          var res = response.json();
          this._monthSalesValue = res.Data;
        });
    }
    if (this._dailyProfit) {
      this.homeService.GetDailyProfit()
        .then(response => {
          var res = response.json();
          this._dailyProfitValue = res.Data;
        });
    }
    if (this._monthProfit) {
      this.homeService.GetMonthprofit()
        .then(response => {
          var res = response.json();
          this._monthProfitValue = res.Data;
        });
    }
    if (this._dailyEntries) {
      this.homeService.GetDailyEntries()
        .then(response => {
          var res = response.json();
          this._dailyEntriesValue = res.Data;
        });
    }
    if (this._monthEntries) {
      this.homeService.GetMonthEntries()
        .then(response => {
          var res = response.json();
          this._monthEntriesValue = res.Data;
        });
    }
  }
}






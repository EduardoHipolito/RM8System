import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';

import { NotificationsService } from '../../_framework/notification/notifications.service';
import { urlStock, urlBase } from '../../_framework/helppers/configs';
import { EntityType } from '../../_domains/enums/EntityType';
import { SupplierType } from '../../_domains/enums/SupplierType';
import { baseComponent } from '../../_framework/helppers/base.component';
import { DropDownSetings, MethodEnum } from '../../_framework/dropdown/DropDownSetings';
import { Sale } from '../../_domains/Sale';
import { ProductSale } from '../../_domains/ProductSale';
import { Payment } from '../../_domains/Payment';
import { CurrencyPipe } from '@angular/common'
import { SaleService } from './sale.service';
import { reduce } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ResponseResult } from '../../_framework/models/ResponseResult';

@Component({
  selector: 'sale',
  templateUrl: './sale.component.html',
  styleUrls: ['./sale.component.css']
})
export class SaleComponent extends baseComponent implements OnInit {
  public url: string = urlStock + "/sale";
  public tabActive: string = 'Customer';
  DdlCustomerSetings: DropDownSetings = new DropDownSetings();
  currentItem: Sale;
  public form: FormGroup;

  constructor(private _fb: FormBuilder, private router: Router, private _saleService: SaleService, private cp: CurrencyPipe, private notificationsService: NotificationsService) {
    super();
  }

  public _customerName: string = "";
  public _totalValue: string = "";
  public _totalPaid: string = "";

  // public get _totalValue() {
  //   return this.currentItem != null ?
  //     this.currentItem.FKCustomer != null ?
  //       this.currentItem.FKCustomer.FKPhysicalPerson != null ?
  //         this.currentItem.FKCustomer.FKPhysicalPerson.Name : '' : '' : '';
  // }

  // public get _totalPaid() {
  //   return this.currentItem != null ?
  //     this.currentItem.FKCustomer != null ?
  //       this.currentItem.FKCustomer.FKPhysicalPerson != null ?
  //         this.currentItem.FKCustomer.FKPhysicalPerson.Name : '' : '' : '';
  // }

  get _formValid(): boolean {
    var totalValue = this.totalProducts(this.currentItem.Products);
    var totalPaid = this.totalPayments(this.currentItem.Payments);
    var isPaid = totalValue == totalPaid;
    return isPaid && (totalValue > 0);
  }



  DropDownCustomerChange(obj: any) {
    this.form.controls['FKCustomer'].setValue(obj);
  }

  public initForm() {
    return this._fb.group({
      Id: [null],
      CreateDate: [null],
      ModifiedDate: [null],
      Status: [EntityType.Ativo, Validators.required],
      IdCustomer: [null, Validators.required],
      FKCustomer: [null],
      Discount: [0],
      Shipping: [0],
      Products: this._fb.array([
        this.createProduct()
      ]),
      Payments: this._fb.array([
        this.createPayment()
      ]),
    });
  }

  createProduct(): FormGroup {
    return this._fb.group({
      Id: [null],
      CreateDate: [null],
      ModifiedDate: [null],
      Status: [EntityType.Ativo, Validators.required],
      IdProduct: [null, Validators.required],
      FKProduct: [null],
      Quantity: [0, Validators.required]
    });
  }
  createPayment(): FormGroup {
    return this._fb.group({
      Id: [null],
      CreateDate: [null],
      ModifiedDate: [null],
      Status: [EntityType.Ativo, Validators.required],
      IdFormOfPayment: [null, Validators.required],
      FKFormOfPayment: [null],
      NSU: [''],
      Value: [0, Validators.required],
      FormOfPaymentRate: [0],
      NumberOfInstallments: [1, Validators.required]
    });
  }

  ngOnInit(): void {
    this.InitializeComponent();
  }

  InitializeComponent() {

    $('form').show();
    this.currentItem = new Sale();
    this.currentItem.Products = new Array<ProductSale>()
    this.currentItem.Products.push(new ProductSale());
    this.currentItem.Payments = new Array<Payment>()
    this.currentItem.Payments.push(new Payment());
    this.form = this.initForm();
    this.DdlCustomerSetings.PropertyValue = 'Id';
    this.DdlCustomerSetings.PropertyText = 'FKPhysicalPerson.Name';
    this.DdlCustomerSetings.Method = MethodEnum.Get;
    this.DdlCustomerSetings.ServiceMethodURL = urlStock + "/customer/GetAll";

    this.onChanges();
  }

  onChanges(): void {
    this.form.valueChanges.subscribe(sale => {
      this.currentItem = sale;
      this._customerName = sale.FKCustomer != null ? sale.FKCustomer.FKPhysicalPerson != null ? sale.FKCustomer.FKPhysicalPerson.Name : '' : '';
      this._totalValue = this.cp.transform(this.totalProducts(sale.Products), 'BRL', true, '1.2-2');
      this._totalPaid = this.cp.transform(this.totalPayments(sale.Payments), 'BRL', true, '1.2-2');
    });
  }

  totalProducts(products: Array<ProductSale>): number {
    var total = 0;
    products.forEach(product => {
      var price = (product.FKProduct != null ? product.FKProduct.Price : 0) * product.Quantity;
      total += price;
    });
    return total;
  }
  totalPayments(payments: Array<Payment>): number {
    var total = 0;
    payments.forEach(product => {
      total += product.Value;
    });
    return total;
  }

  tabChange(val: string) {
    this.tabActive = val;
  }

  ngAfterViewInit() {
    let context = this;

  }

  AddProduct() {
    var products = this.form.get('Products') as FormArray;
    products.push(this.createProduct());


  }

  AddPayment() {
    var payments = this.form.get('Payments') as FormArray;
    payments.push(this.createPayment());
  }

  Save() {
    if (this._formValid) {
      this._saleService.Save(this.currentItem)
        .then(
          response => {
            var res = <ResponseResult>response;
            if (res.State == 1) {
              this.notificationsService.success('Venda realizada com sucesso');
              this.router.navigateByUrl('/RefreshComponent', { skipLocationChange: true }).then(() =>
                this.router.navigate(["/system/stock/sale"]));
            } else {
              this.notificationsService.error('Não foi possivel finalizar a venda');
            }
          },
          error => { }
        );
    } else {
      this.notificationsService.error('A venda não pode ser realizada, confira as informações');
    }
  }
}  
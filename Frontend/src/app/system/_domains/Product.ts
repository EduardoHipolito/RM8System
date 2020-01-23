import { Injectable } from '@angular/core';
import { _EntityBase } from './_EntityBase';
import { Category } from './Category';
import { Supplier } from './Supplier';
import { ProductUnityType } from './enums/ProductUnityType';
import { ProductType } from './enums/ProductType';

@Injectable()
export class Product extends _EntityBase
{
    public Name: number;
    public Color: string;
    public Description: string;
    public IdCategory: number;
    public FKCategory: Category;
    public Brand: string;
    public InternalNumber: string;
    public UnityType: ProductUnityType;
    public BarCode: string;
    public Packing: string;
    public Weight: number;
    public MoreInformation: string;
    public Picture: any;
    public CostPrice: number;
    public Price: number;
    public MinPrice: number;
    public ProductType: ProductType;
}

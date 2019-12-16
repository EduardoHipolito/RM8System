import { FormGroup } from "@angular/forms";
import { baseCrudComponent } from "../helppers/baseCrud.component";
import { baseComponent } from "../helppers/base.component";
import { CrudplugComponent } from "../crudplug/crudplug.component";
import { RequestBase } from "../models/RequestBase";
import { GridComponent } from "./grid.component";

export class GridSetings {
    public gridComponentOnLoadCallback: (gridContext: GridComponent) => void;
    public columns: Array<GridColumnSetings>;
    public paging: boolean;
    context: baseComponent;
    gridContext: GridComponent;
    crudPlugContext: CrudplugComponent;
    public form: FormGroup;
    public rows: Array<any>;
    public data: Array<any>;
    public TotalItems: number = 0;
    public RequestSettings: GridRequestSettings;

    constructor() {
        this.rows = new Array<any>();
        this.columns = new Array<GridColumnSetings>();
        this.RequestSettings = new GridRequestSettings();
    }
}

export class GridColumnSetings {
    public title: string;
    public name: string;
    public sort: GridSortSetings;
    public filter: GridFilterSetings;
    public type: string;
    public modalId: string;
    public boolean: BooleanType;
    public alt: string;
    public height: string;
    public buttonCallback: (row: any) => void;
    public buttonIcon: string;
    public buttonCollor: string;
    public modalCallback: (Id: number, context: baseComponent, modalId: string, FinishAndOpenModalCallback: (modalId: string) => void) => void;
    public sortName: string;
    public customRender: (row: any, context: baseComponent) => string;
}

export class GridFilterSetings {
    public filterString: string;
    public placeholder: string;
}

export class GridSortSetings {
    public direction: string;
    public able: boolean = true;
    public current: boolean;
}

export class BooleanType {
    public true: any;
    public false: any;
}
export class GridRequestSettings {

    public CurrentPage: number = 1;
    public ItemsPerPage: number = 8;
    public ColumOrder: string;
    public OrderDirection: string;
    public Filter: any;
}
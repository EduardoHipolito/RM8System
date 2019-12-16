import { GridSetings } from "../grid/GridSetings";
import { FormGroup } from "@angular/forms";
import { baseCrudComponent } from "../helppers/baseCrud.component";

export class CrudPlugSetings {
    public Url: string;
    public Eraser: boolean;
    public Add: boolean;
    public Edit: boolean;
    public Delete: boolean;
    public Search: boolean;
    public Refresh: boolean;
    public Grid: boolean;
    public Form: boolean;
    public formG: FormGroup;
    public changeCallBack: (any, baseCrudComponent) => void;
    public getAllCallBack: (any, baseCrudComponent, type) => void;
    public formEntity: any;
    public context: baseCrudComponent;
    public gridSettings: GridSetings;

}


import { RequestBase, RequestBaseParameter } from "./RequestBase";
import { GridRequestSettings } from "../grid/GridSetings";

export class RequestGridParameter<TParameter> extends RequestBaseParameter<TParameter>
{
    public Settings: GridRequestSettings;
}

export class RequestGrid extends RequestBase {
    public Settings: GridRequestSettings;
}
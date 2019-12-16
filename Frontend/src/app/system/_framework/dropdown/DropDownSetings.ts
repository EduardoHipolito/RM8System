
export class DropDownSetings {
    public PropertyValue: string = 'Value';
    public PropertyText: string = 'Text';
    public DataSource: Array<any>;
    public ServiceMethodURL: string;
    public Method: MethodEnum;
    public Parameter: any;
}

export enum MethodEnum {
    Get,
    Post
}
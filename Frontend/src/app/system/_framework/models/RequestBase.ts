export class RequestBase {
    public Identity: any;
    constructor(){
        this.Identity = {};
    }
}

export class RequestBaseParameter<T> extends RequestBase {
    public Parameter: T;
    constructor(){
        super();
    }
}

export class Methods {
    public static CloneObject(value: any): any {
        return JSON.parse(JSON.stringify(value));
    }

    public static EnumToArray<TEnum, TKeys extends string>(E: { [key in TKeys]: TEnum }): Array<{ Value: number; Text: string }> {
        let map: { Value: number; Text: string }[] = [];

        let keys = Object.keys(E) as Array<TKeys>;
        Object.values(E)

        keys.forEach(k => {
            if (E[k as string] > 0) {
                map.push({ Value: E[k as any], Text: k.replace('_', ' ') })
            }
        });

        return map;
    }

    public static stringToNumber(value) {
        return Number(value.toString().trim().replace(/ /g,'').replace(' ','').replace(',', '.'));
      }
}

export enum MethodEnum {
    Get,
    Post
}
import { Injectable } from '@angular/core';

@Injectable()
export class AuthCookie {

  constructor() { }

  public clearStorage() {
    localStorage.clear();
  }

  //LOCAL STORAGE WITHOUT CIRCULAR DEPENDENCE
  public SetIntoLocalStorage(name: string, value: any, removeCircularDependence: boolean = false) {
    let jsonValue: string;
    if (removeCircularDependence) {
      var cache = [];
      jsonValue = JSON.stringify(value, function (key, value) {
        if (typeof value === 'object' && value !== null) {
          if (cache.indexOf(value) !== -1) {
            return;
          }
          cache.push(value);
        }
        return value;
      });
      cache = null;
    } else {
      jsonValue = JSON.stringify(value);
    }
    localStorage.setItem(btoa(name), btoa(jsonValue));
  }

  public GetIntoLocalStorage<T>(name: string) {
     let criptStorage = localStorage.getItem(btoa(name));
    if (criptStorage == null || criptStorage.length <= 0) {
      return null;
    } else {
      return <T>JSON.parse(atob(criptStorage));
    }
  }
}
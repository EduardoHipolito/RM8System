import { Injectable } from '@angular/core';

@Injectable()
export class PageLoaderService {

  constructor() { }

  public LoadOn() {
    document.getElementById("preloader").style.display = "block";
  }
  public LoadOff() {
    document.getElementById("preloader").style.display = "none";
  }


}
 
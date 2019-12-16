import { Component } from '@angular/core';
import { containsElement } from '@angular/animations/browser/src/render/shared';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'RM8';
  constructor(){
    console.log('-oooooooo/-      .+ooooooooo:  +ooo+        oooo/');
    console.log('+MMMMMMMMMMm+   -NMMMMMMMMMMs  +MMMM:      /MMMM/');
    console.log('+MMMNyyydMMMMy  /MMMMyyyyyyy/   mMMMd      mMMMd');
    console.log('+MMMm    :MMMM. /MMMN           /MMMM/    /MMMM:');
    console.log('+MMMm    .MMMM- /MMMN            dMMMm    mMMMh');
    console.log('+MMMm    .MMMM- /MMMMyyyy+       :MMMM/  +MMMM-');
    console.log('+MMMm    .MMMM- /MMMMMMMMy        hMMMm  NMMMy');
    console.log('+MMMm    .MMMM- /MMMMoooo:        -MMMM+oMMMM-');
    console.log('+MMMm    .MMMM- /MMMN              yMMMmNMMMy');
    console.log('+MMMm    +MMMM. /MMMN              .MMMMMMMM.');
    console.log('+MMMMdddNMMMMo  /MMMMddddddd+       sMMMMMMs');
    console.log('+MMMMMMMMMNh:   .mMMMMMMMMMMs        yMMMMs');
  }
}

import { Component } from '@angular/core';
import {Router} from '@angular/router'
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  title = 'Manoj';

constructor(private router:Router){
  if(localStorage.userData){
    this.router.navigateByUrl("/student");
  }
  else{
    this.router.navigateByUrl("/login");
  }
}






}

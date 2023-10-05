import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'splitify';
  isUserLogged = false;
  
  ngOnInit(): void {
    this.isUserLogged = localStorage.getItem('AUTH_TOKEN') !== null;
  }
}

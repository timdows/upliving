import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-daily-picture',
  templateUrl: './daily-picture.component.pug',
  styleUrls: ['./daily-picture.component.scss']
})
export class DailyPictureComponent implements OnInit {
  
  constructor() { }

  ngOnInit() {
    // var rellaxHeader = new Rellax('.rellax-header');

    var body = document.getElementsByTagName('body')[0];
    body.classList.add('landing-page');
    var navbar = document.getElementsByTagName('nav')[0];
    navbar.classList.add('navbar-transparent');
  }
  ngOnDestroy(){
    var body = document.getElementsByTagName('body')[0];
    body.classList.remove('landing-page');
    var navbar = document.getElementsByTagName('nav')[0];
    navbar.classList.remove('navbar-transparent');
  }

}

import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.pug',
	styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

	data: Date = new Date();
	focus;
	focus1;

	siteName: string = "Upliving";

	constructor() { }

	ngOnInit() {
		this.siteName = window.location.hostname;

		// var rellaxHeader = new Rellax('.rellax-header');

		var body = document.getElementsByTagName('body')[0];
		body.classList.add('landing-page');
		var navbar = document.getElementsByTagName('nav')[0];
		navbar.classList.add('navbar-transparent');
	}
	ngOnDestroy() {
		var body = document.getElementsByTagName('body')[0];
		body.classList.remove('landing-page');
		var navbar = document.getElementsByTagName('nav')[0];
		navbar.classList.remove('navbar-transparent');
	}

}

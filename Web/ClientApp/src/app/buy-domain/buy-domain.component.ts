import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'app-buy-domain',
	templateUrl: './buy-domain.component.pug',
	styleUrls: ['./buy-domain.component.scss']
})
export class BuyDomainComponent implements OnInit {

	siteName: string = "Upliving.nl";

	constructor() { }

	ngOnInit() {
		this.siteName = window.location.host;
	}

}

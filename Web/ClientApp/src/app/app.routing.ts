import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { DailyPictureComponent } from './daily-picture/daily-picture.component';
import { ProfileComponent } from './profile/profile.component';
import { HomeComponent } from './home/home.component';
import { BuyDomainComponent } from './buy-domain/buy-domain.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'dailypictures', component: DailyPictureComponent },
    { path: 'abouttim', component: ProfileComponent },
    { path: 'buydomain', component: BuyDomainComponent },
    { path: '**', redirectTo: '/' }
];

@NgModule({
    imports: [
        CommonModule,
        BrowserModule,
        RouterModule.forRoot(routes)
    ],
    exports: [
    ],
})
export class AppRoutingModule { }

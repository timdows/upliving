import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { ComponentsComponent } from './components/components.component';
import { LandingComponent } from './examples/landing/landing.component';
import { LoginComponent } from './examples/login/login.component';
import { ProfileComponent } from './examples/profile/profile.component';
import { NucleoiconsComponent } from './components/nucleoicons/nucleoicons.component';
import { DailyPictureComponent } from './daily-picture/daily-picture.component';

const routes: Routes = [
    { path: '', component: LandingComponent },
    { path: 'dailypictures', component: DailyPictureComponent },
    { path: 'examples/landing', component: LandingComponent },
    { path: 'examples/login', component: LoginComponent },
    { path: 'examples/profile', component: ProfileComponent },
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

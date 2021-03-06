import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // this is needed!
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';

import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { DailyPictureComponent } from './daily-picture/daily-picture.component';
import { ProfileComponent } from './profile/profile.component';
import { FooterComponent } from './shared/footer/footer.component';
import { ApiModule, Configuration, ConfigurationParameters } from 'api';
import { environment } from 'environments/environment';
import { HttpClientModule } from '@angular/common/http';
import { BuyDomainComponent } from './buy-domain/buy-domain.component';
import { HomeComponent } from './home/home.component';

export function apiConfigFactory(): Configuration {
    const params: ConfigurationParameters = {
        basePath: environment.serverUrl,
        apiKeys: {}
    }
    return new Configuration(params);
}

@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent,
        DailyPictureComponent,
        ProfileComponent,
        FooterComponent,
        BuyDomainComponent,
        HomeComponent
    ],
    imports: [
        ApiModule.forRoot(apiConfigFactory),
        HttpClientModule,
        BrowserAnimationsModule,
        NgbModule.forRoot(),
        FormsModule,
        RouterModule,
        AppRoutingModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }

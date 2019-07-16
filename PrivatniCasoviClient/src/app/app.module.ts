import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app.routing';
import { MsAdalAngular6Module } from 'microsoft-adal-angular6';
import { AuthenticationGuard } from 'microsoft-adal-angular6';
import { AppComponent } from './app.component';
import { SignupComponent } from './signup/signup.component';
import { LandingComponent } from './landing/landing.component';
import { ProfileComponent } from './profile/profile.component';
import { HomeComponent, NgbdModalContent } from './home/home.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { FooterComponent } from './shared/footer/footer.component';

import { HomeModule } from './home/home.module';
import { TokenInterceptor } from './token.interceptor';
import { TestService } from './test-service.service';
import { MyComponentComponent } from './my-component/my-component.component';
import { UserService } from './services/user.service';
import { PrivateClassService } from './services/private-class.service';
import { FullCalendarModule } from '@fullcalendar/angular';
import { NouisliderModule } from 'ng2-nouislider';
import { MathematicsComponent } from './mathematics/mathematics.component';
import { ProgrammingComponent } from './programming/programming.component';
import { ElectrotechnicsComponent } from './electrotechnics/electrotechnics.component';

export function getAdalConfig() {
  return {
      tenant: 'privatnicasovi.onmicrosoft.com',
      clientId: '6dcc771c-87bc-4168-b1e3-c6b38ea7c953',
      redirectUri: 'http://localhost:5600/',
      audience: 'https://privatnicasovi.onmicrosoft.com/PrivatniCasoviFTN',
      // endpoints: {
      //   "https://localhost/Api/": "xxx-bae6-4760-b434-xxx",
      // },
      navigateToLoginRequestUrl: false,
      cacheLocation: 'localStorage',
    };
}

@NgModule({
  declarations: [
    AppComponent,
    SignupComponent,
    LandingComponent,
    ProfileComponent,
    NavbarComponent,
    FooterComponent,
    MyComponentComponent,
    NgbdModalContent,
    MathematicsComponent,
    ProgrammingComponent,
    ElectrotechnicsComponent,
    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    NouisliderModule,
    ReactiveFormsModule,
    NgbModule.forRoot(),
    FormsModule,
    HttpClientModule,
    RouterModule,
    FullCalendarModule,
    AppRoutingModule,
    HomeModule,
    MsAdalAngular6Module.forRoot(getAdalConfig),
  ],
  providers: [AuthenticationGuard, {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true},
              TestService, UserService, PrivateClassService],
  bootstrap: [AppComponent],
  entryComponents: [NgbdModalContent],
})
export class AppModule { }

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MsAdalAngular6Module } from 'microsoft-adal-angular6';
import { AuthenticationGuard } from 'microsoft-adal-angular6';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TokenInterceptor } from './token.interceptor';
import { TestService } from './test.service';

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
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MsAdalAngular6Module.forRoot(getAdalConfig),
  ],
  providers: [AuthenticationGuard, {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}, TestService],
  bootstrap: [AppComponent]
})
export class AppModule { }

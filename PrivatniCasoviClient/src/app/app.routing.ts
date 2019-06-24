import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { SignupComponent } from './signup/signup.component';
import { LandingComponent } from './landing/landing.component';
import { NucleoiconsComponent } from './components/nucleoicons/nucleoicons.component';
import { AuthenticationGuard } from 'microsoft-adal-angular6';
import { MyComponentComponent } from './my-component/my-component.component';

const routes: Routes = [
    { path: 'home',             component: HomeComponent , canActivate: [AuthenticationGuard]},
    { path: 'my',             component: MyComponentComponent , canActivate: [AuthenticationGuard]},
    { path: 'user-profile',     component: ProfileComponent , canActivate: [AuthenticationGuard]},
    { path: 'signup',           component: SignupComponent , canActivate: [AuthenticationGuard]},
    { path: 'landing',          component: LandingComponent },
    { path: 'nucleoicons',      component: NucleoiconsComponent , canActivate: [AuthenticationGuard]},
    { path: '', redirectTo: 'landing', pathMatch: 'full' },
    { path: '**', redirectTo: 'landing', pathMatch: 'full' }
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

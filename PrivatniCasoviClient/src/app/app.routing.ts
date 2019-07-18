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
import { MathematicsComponent } from './mathematics/mathematics.component';
import { ElectrotechnicsComponent } from './electrotechnics/electrotechnics.component';
import { ProgrammingComponent } from './programming/programming.component';
import { SubjectDetailsComponent } from './subject-details/subject-details.component';
import { ProfileDetailsComponent } from './profile-details/profile-details.component';

const routes: Routes = [
    { path: 'home',             component: HomeComponent , canActivate: [AuthenticationGuard]},
    { path: 'my',             component: MyComponentComponent , canActivate: [AuthenticationGuard]},
    { path: 'user-profile',     component: ProfileComponent , canActivate: [AuthenticationGuard]},
    { path: 'signup',           component: SignupComponent , canActivate: [AuthenticationGuard]},
    { path: 'mathematics',           component:  MathematicsComponent , canActivate: [AuthenticationGuard]},
    { path: 'electrotehnics',           component:  ElectrotechnicsComponent , canActivate: [AuthenticationGuard]},
    { path: 'programming',           component:  ProgrammingComponent , canActivate: [AuthenticationGuard]},
    { path: 'subject-details',           component:  SubjectDetailsComponent , canActivate: [AuthenticationGuard]},
    { path: 'profile-details',           component:  ProfileDetailsComponent , canActivate: [AuthenticationGuard]},
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

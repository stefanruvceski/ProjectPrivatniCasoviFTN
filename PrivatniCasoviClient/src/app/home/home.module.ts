import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';

import { HomeComponent, NgbdModalContent } from './home.component';

import { ComponentsModule } from '../components/components.module';
import { FullCalendarModule } from '@fullcalendar/angular';
import { NouisliderModule } from 'ng2-nouislider';

@NgModule({
    imports: [
        CommonModule,
        BrowserModule,
        FormsModule,
        RouterModule,
        NouisliderModule,
        FullCalendarModule,
        ComponentsModule,
    ],
    declarations: [ HomeComponent ],
    exports: [ HomeComponent ],

    providers: []
})
export class HomeModule { }

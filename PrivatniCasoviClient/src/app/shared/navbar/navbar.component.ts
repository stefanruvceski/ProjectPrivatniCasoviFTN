import { Component, OnInit, ElementRef } from '@angular/core';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { UserService } from 'app/services/user.service';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
    private toggleButton: any;
    private sidebarVisible: boolean;
    username: string;
    image:string;
    
    constructor(public location: Location, private element: ElementRef, private userService: UserService,
        private adalSvc: MsAdalAngular6Service) {
        this.sidebarVisible = false;
    }

    ngOnInit() {
        const navbar: HTMLElement = this.element.nativeElement;
        this.toggleButton = navbar.getElementsByClassName('navbar-toggler')[0];
        this.userService.username.subscribe ((newUsername) => {this.username = newUsername;  });
        this.userService.image.subscribe ((newImage) => {this.image = newImage;  });
    }

    logout() {
        window.localStorage.clear();
        this.adalSvc.logout();
    }
    sidebarOpen() {
        const toggleButton = this.toggleButton;
        const html = document.getElementsByTagName('html')[0];
        // console.log(html);
        // console.log(toggleButton, 'toggle');

        setTimeout(function(){
            toggleButton.classList.add('toggled');
        }, 500);
        html.classList.add('nav-open');

        this.sidebarVisible = true;
    };
    sidebarClose() {
        const html = document.getElementsByTagName('html')[0];
        // console.log(html);
        this.toggleButton.classList.remove('toggled');
        this.sidebarVisible = false;
        html.classList.remove('nav-open');
    };
    sidebarToggle() {
        // const toggleButton = this.toggleButton;
        // const body = document.getElementsByTagName('body')[0];
        if (this.sidebarVisible === false) {
            this.sidebarOpen();
        } else {
            this.sidebarClose();
        }
    };
    isHome() {
        const titlee = this.location.prepareExternalUrl(this.location.path());

        if ( titlee === '/home' ) {
            return true;
        } else {
            return false;
        }
    }
    IsLanding() {
        const titlee = this.location.prepareExternalUrl(this.location.path());
        if ( titlee === '/landing' ) {
            return true;
        } else {
            return false;
        }
    }
}

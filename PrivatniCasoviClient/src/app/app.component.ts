import { Component } from '@angular/core';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';
import { TestService } from './test.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'PrivatniCasoviFTN';
  constructor(private adalSvc: MsAdalAngular6Service, private servis: TestService) {
    console.log(this.adalSvc.userInfo);
    const token = this.adalSvc.acquireToken('https://graph.microsoft.com').subscribe((tokenn: string) => {
      console.log(tokenn);
      localStorage.setItem('jwt', tokenn);
    });
  }

  onclick() {
    this.servis.fja().subscribe(stri => {
      alert(stri);
      console.log(stri);
    },
    error => {
alert(error);

    });
  }
}

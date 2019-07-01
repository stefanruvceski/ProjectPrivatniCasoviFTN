import { Component, OnInit } from '@angular/core';
import { TestService } from 'app/test-service.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
    model = {
        left: true,
        middle: false,
        right: false
    };

    focus;
    focus1;
    constructor(private service: TestService) {

        this.service.fja().subscribe(data => alert(data));
    }

    ngOnInit() {}
}

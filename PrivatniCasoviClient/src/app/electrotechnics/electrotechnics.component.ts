import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/services/user.service';
import { User } from 'app/Model/User';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-electrotechnics',
  templateUrl: './electrotechnics.component.html',
  styleUrls: ['./electrotechnics.component.scss']
})
export class ElectrotechnicsComponent implements OnInit {

  constructor(private userService: UserService) { }
  electroTeachers: Observable<User>;
  ngOnInit() {
    this.getAllElectroTeachers();
  }

  getAllElectroTeachers(){
    this.userService.getAllTeachers('ELECTROTEHNICS').subscribe(data=>{
      console.log(data);
      this.electroTeachers = data;
    });
  }


}

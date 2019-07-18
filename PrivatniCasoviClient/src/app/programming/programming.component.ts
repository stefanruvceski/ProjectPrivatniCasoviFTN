import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/services/user.service';
import { Observable } from 'rxjs';
import { User } from 'app/Model/User';

@Component({
  selector: 'app-programming',
  templateUrl: './programming.component.html',
  styleUrls: ['./programming.component.scss']
})
export class ProgrammingComponent implements OnInit {

  constructor(private userService: UserService) { }
  progTeachers: Observable<User>;
  ngOnInit() {
    this.getAllProgTeachers();
  }

  getAllProgTeachers(){
    this.userService.getAllTeachers('PROGRAMMING').subscribe(data=>{
      console.log(data);
      this.progTeachers = data;
    });
  }

}

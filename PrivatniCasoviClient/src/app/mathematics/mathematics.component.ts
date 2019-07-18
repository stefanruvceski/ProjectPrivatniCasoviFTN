import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/services/user.service';
import { Observable } from 'rxjs';
import { User } from 'app/Model/User';


@Component({
  selector: 'app-mathematics',
  templateUrl: './mathematics.component.html',
  styleUrls: ['./mathematics.component.scss']
})
export class MathematicsComponent implements OnInit {

  constructor(private userService: UserService) { }
  mathTeachers: Observable<User>;
  ngOnInit() {
    this.getAllMathTeachers();
  }

  getAllMathTeachers(){
    this.userService.getAllTeachers('MATHEMATICS').subscribe(data=>{
      console.log(data);
      this.mathTeachers = data;
    });
  }

}

import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/services/user.service';
import { Observable } from 'rxjs';
import { User } from 'app/Model/User';
import { DataService } from 'app/services/data.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-mathematics',
  templateUrl: './mathematics.component.html',
  styleUrls: ['./mathematics.component.scss']
})
export class MathematicsComponent implements OnInit {

  constructor(private route: Router, private userService: UserService,private dataService: DataService) { }
  mathTeachers: Observable<User>;
  message: string;
  ngOnInit() {
    this.getAllMathTeachers();
    this.dataService.currentMessage.subscribe(data => this.message = data);
  }

  getAllMathTeachers(){
    this.userService.getAllTeachers('MATHEMATICS').subscribe(data=>{
      console.log(data);
      this.mathTeachers = data;
    });
  }

  onTeacherClick(teacher: User){
    this.dataService.changeUser(teacher);
    this.route.navigate(['/profile-details']);
  }

  onSubjectClick(subject: string){
    
    this.dataService.changeMessage(subject);
    this.route.navigate(['/subject-details']);
  }

}

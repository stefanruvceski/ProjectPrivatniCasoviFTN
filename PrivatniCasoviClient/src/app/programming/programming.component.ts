import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/services/user.service';
import { Observable } from 'rxjs';
import { User } from 'app/Model/User';
import { Router } from '@angular/router';
import { DataService } from 'app/services/data.service';

@Component({
  selector: 'app-programming',
  templateUrl: './programming.component.html',
  styleUrls: ['./programming.component.scss']
})
export class ProgrammingComponent implements OnInit {
  message: string;
  constructor(private route: Router, private userService: UserService,private dataService: DataService) { }
  progTeachers: Observable<User>;
  ngOnInit() {
    this.getAllProgTeachers();
    this.dataService.currentMessage.subscribe(data => this.message = data);
  }

  getAllProgTeachers(){
    this.userService.getAllTeachers('PROGRAMMING').subscribe(data=>{
      console.log(data);
      this.progTeachers = data;
    });
  }

  onSubjectClick(subject: string){
    
    this.dataService.changeMessage(subject);
    this.route.navigate(['/subject-details']);
  }

  onTeacherClick(teacher: User){
    this.dataService.changeUser(teacher);
    this.route.navigate(['/profile-details']);
  }
}

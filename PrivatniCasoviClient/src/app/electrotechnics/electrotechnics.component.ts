import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/services/user.service';
import { User } from 'app/Model/User';
import { Observable } from 'rxjs';
import { DataService } from 'app/services/data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-electrotechnics',
  templateUrl: './electrotechnics.component.html',
  styleUrls: ['./electrotechnics.component.scss']
})
export class ElectrotechnicsComponent implements OnInit {
  message: string;
  constructor(private route: Router, private userService: UserService, private dataService: DataService) { }
  electroTeachers: Observable<User>;
  ngOnInit() {
    this.getAllElectroTeachers();
    this.dataService.currentMessage.subscribe(data => this.message = data);
  }

  getAllElectroTeachers(){
    this.userService.getAllTeachers('ELECTROTEHNICS').subscribe(data=>{
      console.log(data);
      this.electroTeachers = data;
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

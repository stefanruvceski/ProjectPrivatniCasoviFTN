import { Component, OnInit } from '@angular/core';
import { Subject } from 'app/Model/Subject';
import { TeacherSubject } from 'app/Model/TeacherSubject';
import { DataService } from 'app/services/data.service';
import { SubjectService } from 'app/services/subject.service';
import { User } from 'app/Model/User';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-profile-details',
  templateUrl: './profile-details.component.html',
  styleUrls: ['./profile-details.component.scss']
})
export class ProfileDetailsComponent implements OnInit {
  user: User;
  subjects: TeacherSubject;
  constructor(private dataService: DataService,private subjectService: SubjectService) { }

  ngOnInit() {
    this.dataService.currentUser.subscribe(data => {
      this.user = data;
      this.subjectService.getTeacherSubjects(data.Id).subscribe(data2=>{
        this.subjects = data2;
        console.log(data2);
      });
    });
  }

}

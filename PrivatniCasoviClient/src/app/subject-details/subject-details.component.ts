import { Component, OnInit } from '@angular/core';
import { DataService } from 'app/services/data.service';
import { SubjectService } from 'app/services/subject.service';
import { Subject } from 'app/Model/Subject';


@Component({
  selector: 'app-subject-details',
  templateUrl: './subject-details.component.html',
  styleUrls: ['./subject-details.component.scss']
})
export class SubjectDetailsComponent implements OnInit {
  subject: Subject;
  message: string;
  constructor(private dataService: DataService,private subjectService: SubjectService) { }

  ngOnInit() {
    this.dataService.currentMessage.subscribe(data => {
      this.message = data;
      this.subjectService.getSubjectByName(data.split('?')[0]).subscribe(data2 => {
        this.subject = data2;
        console.log(this.subject);
      });
    });
  }

}

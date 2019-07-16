import { Component, OnInit, ViewChild } from '@angular/core';
import { OptionsInput } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import { FullCalendarComponent } from '@fullcalendar/angular';
import interactionPlugin from '@fullcalendar/interaction';
import { Calendar } from '@fullcalendar/core';;
import timeGridPlugin from '@fullcalendar/timegrid';
import listPlugin from '@fullcalendar/list';

import { TestService } from 'app/test-service.service';
import { NgbModal, NgbActiveModal, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { User } from 'app/Model/User';
import { UserService } from 'app/services/user.service';
import { PrivateClassService } from 'app/services/private-class.service';
import { Observable, observable } from 'rxjs';
import { PrivateClass } from 'app/Model/PrivateClass';
import { element } from '@angular/core/src/render3';

import { reduce } from 'rxjs/operators';
import { SubjectService } from 'app/services/subject.service';
import { AddPrivateClass } from 'app/Model/AddPrivateClass';
import { AlertPromise } from 'selenium-webdriver';
import { temporaryAllocator } from '@angular/compiler/src/render3/view/util';


@Component({
    selector: 'app-home',
    styleUrls: ['./home.component.scss'],
    template: `
  <div style="background: rgba(0, 0, 0, 0.6); color:white;">
    <div class="modal-header" style="background-color: rgba(0, 0, 0, 0.6);color:white;">
        <h5 *ngIf="name.isEvent" class="modal-title text-center"  style="color:white;">Class informations</h5>
        <h5 *ngIf="!name.isEvent" class="modal-title text-center"  style="color:white;">Choose class</h5>
        
        <button type="button" class="close" aria-label="Close" (click)="activeModal.dismiss('Cross click')">
        <span aria-hidden="true">&times;</span>
        </button>
    </div>
    <div class="modal-body" style="background-color: rgba(0, 0, 0, 0.6);color:white;">
    <div class="container-fluid">
  <div class="row no-gutter">
            <div class="col-lg-9 mx-auto">
            <form *ngIf="info ==''"  class="form-signin" [formGroup]="profileForm" (submit)="onSubmit()">
              <div class="form-label-group">
              <label for="inputUsername">Username</label>
                <input  type="text" id="inputUsername" formControlName="username" class="form-control" placeholder="Username" autofocus>
              </div><br>
              <div class="form-label-group">
              <label for="inputFirstName">First Name</label>
                <input type="text" id="inputFirstName" formControlName="firstName" class="form-control" placeholder="First Name" >
              </div><br>
              <div class="form-label-group">
              <label for="inputLastName">Last Name</label>
              <input type="text" id="inputLastName" formControlName="lastName" class="form-control" placeholder="Last Name" >
            </div><br>
            <div class="form-label-group">
            <label for="inputAddress">Address</label>
                <input type="text" id="inputAddress" formControlName="address" class="form-control" placeholder="Address" >
              </div>
              <br>
              <div class="form-label-group">
              <label for="inputPhone">Phone</label>
                <input type="text" id="inputPhone" formControlName="phone" class="form-control" placeholder="Phone" >
              </div>
<br>
              <div class="form-label-group">
              <label for="inputEmail">Preffer Email</label>
                <input type="email" id="inputPhone" formControlName="email" class="form-control" placeholder="Preffer Email" >
              </div><br>
              <div class="form-label-group">
              <label for="inputDegree">Degree of Education</label>
                <input type="text" id="inputDegree" formControlName="degree" class="form-control" placeholder="Degree of Education" >
              </div>
              <br><br>
              <button class="btn btn-lg btn-outline-danger btn-block btn-round text-uppercase font-weight-bold mb-2" [disabled]="profileForm.invalid"  type="submit">Send informations</button>
            </form>
            <div *ngIf="!name.isEvent && info !=''">
            <form class="form-signin" [formGroup]="profileForm2" (submit)="onSubmit()">
            
         
             <h6 style="text-align:center">Date {{name.date}}</h6>
             <h6 *ngIf="name.time!='no'" style="text-align:center">Time {{name.time}}</h6>
           
            <br>
            <div class="form-label-group ">
            <h6 >Subject</h6>
            <select class="form-control" formControlName="subject" id="lab" required (change)="onchange()">
               <option *ngFor="let subject of subjects" value="{{subject}}"   [ngValue]="type">{{subject}}</option> 
               
            </select>
        </div>
            <br>

            <div class="form-label-group">
            <h6 for="inputEmail">Lesson</h6>
            <input type="text" id="inputLesson" formControlName="lesson" class="form-control" placeholder="lesson"  autofocus>
            
          </div>
         
          </form> 
          <br>
          <div *ngIf="isSecretary()">
          <h6 >Number of Students: {{simpleSlider}}</h6>
          <nouislider [connect]="false"  [min]="1" (change)="onChangeSlider($event)" [max]="4" [step]="1" [(ngModel)]="simpleSlider" [tooltips]="false" class="slider"></nouislider>
          </div>
          <div *ngIf="name.time=='no'">
          <br>         
           <h6>Time</h6>
          
            
            <ngb-timepicker [formControl]="ctrl" class="text-center center" style="horizontal-align: center;" [(ngModel)]="time" [hourStep]="hourStep" [minuteStep]="minuteStep" ></ngb-timepicker>
            <div *ngIf="ctrl.valid" class="small form-text text-success">Great choice</div>
            <div class="small form-text text-danger" *ngIf="!ctrl.valid">
              <div *ngIf="ctrl.errors['tooLate']">Oh no, it's too late</div>
              <div *ngIf="ctrl.errors['tooEarly']">It's a bit too early</div>
            </div>
          </div>
            </div>
            <div *ngIf="name.isEvent && info !=''">
              <table>
              <tr>
              <td colspan="2">Status</td>
              <td></td>
                  <td>{{name.status}}</td>
                </tr>
                <tr>
                <td colspan="2">Title</td>
                <td></td>
                  <td>{{name.title}}</td>
                </tr>
                <tr>
                <td colspan="2">Lesson</td>
                <td></td>
                  <td>{{name.lesson}}</td>
                </tr>
                <tr>
                <td colspan="2">Class start</td>
                <td></td>
                  <td>{{name.startDate}}</td>
                </tr>
                <tr>
                  <td colspan="2">Class End</td>
                  <td></td>
                  <td style="width:190px;float:right">{{name.endDate}}</td>
                </tr>
                <tr *ngIf="name.status != REQUESTED">
                <td colspan="2">Teacher</td>
                <td></td>
                  <td>{{name.teacher}}</td>
                </tr>
                <tr>
                <td colspan="2">Students</td>
                <td></td>
                  <td>{{name.numOfStud}}</td>
                </tr>
                <tr *ngIf="isSecretary() && name.status != 'ACCEPTED'">
                <td colspan="2">Teacher</td>
                <td></td>
                  <td>
                    <select required >
                      <option *ngFor="let teacher of teachers" value="{{teacher}}" (change)="onChangeTeacher($event.target.innerText)"  [ngValue]="type">{{teacher}}</option>
                    </select>
                  </td>
                </tr>
              </table>

            </div>
            
          </div>
    </div></div></div></div>
    <div *ngIf="info !=''" class="modal-footer">
        <div class="left-side">
            <button type="button" class="btn btn-default btn-link" (click)="activeModal.close('Close click')">Never mind</button>
        </div>
        <div class="divider"></div>
        <div class="right-side">
            <button *ngIf="name.isEvent && name.isMine=='no' && isStudent()"button type="button" class="btn btn-success btn-link" (click)="joinClass(name.id)">Join class</button>
            <button *ngIf="name.isEvent && name.isMine=='no' && isTeacher()"button type="button" class="btn btn-success btn-link" (click)="acceptClass(name.id)">Accept class</button>
            <button *ngIf="name.isEvent && name.isMine=='yes' && isStudent()"button type="button" class="btn btn-danger btn-link" (click)="studentDeclineClass(name.id)">Decline class</button>
            <button *ngIf="name.isEvent && name.isMine=='yes' && isTeacher()"button type="button" class="btn btn-danger btn-link" (click)="teacherDeclineClass(name.id)">Decline class</button>
            <button *ngIf="!name.isEvent && isStudent()"button type="button" class="btn btn-success btn-link" (click)="requestClass()">Request class</button>
            <div class="row"><div class="col">
            <button *ngIf="name.isEvent && isSecretary() && name.status != 'ACCEPTED'" button type="button" class="btn btn-success btn-link" (click)="assignClass(name.id)">Assign</button>
            <button *ngIf="!name.isEvent && isSecretary()"button type="button" class="btn btn-success btn-link" (click)="secretaryRequestClass()">Request class</button>
            </div> <div class="divider"></div><div class="col">
            <button *ngIf="name.isEvent && name.status != 'DECLINED' && isSecretary()" button type="button" class="btn btn-danger btn-link" (click)="secretaryDeclineClass(name.id)">Decline</button>
            </div></div>
            </div>
    </div>
    `
})
export class NgbdModalContent {
    user: User;
    simpleSlider  = 1;
    name: any;
    ctrl = new FormControl('', (control: FormControl) => {
      const value = control.value;
  
      if (!value) {
        return null;
      }
  
      if (value.hour < 8) {
        return {tooEarly: true};
      }
      if (value.hour > 22) {
        return {tooLate: true};
      }
  
      return null;
    });
    teacher: string;
    teachers: Observable<string>;
    privateClass: AddPrivateClass;
    subjects: Observable<string>;
    profileForm2 = this.fb.group({
        id: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
        subject: ['', [ Validators.minLength(3), Validators.maxLength(30)]],
        lesson: ['', [ Validators.minLength(3), Validators.maxLength(30)]],
        numOfStudents:  ['', [ Validators.minLength(3), Validators.maxLength(30)]],

        });
        time: NgbTimeStruct = {hour: 13, minute: 30, second: 0};
        hourStep = 1;
        minuteStep = 30;
        profileForm = this.fb.group({
          username: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
          firstName: ['', [ Validators.minLength(3), Validators.maxLength(30)]],
          lastName: ['', [ Validators.minLength(3), Validators.maxLength(30)]],
          address: ['', [, Validators.minLength(3), Validators.maxLength(30)]],
          phone: ['', [ Validators.minLength(3), Validators.maxLength(30)]],
          email: ['', [Validators.minLength(3), Validators.maxLength(30)]],
          degree: ['', [ Validators.minLength(3), Validators.maxLength(30)]],
          });
    constructor( public activeModal: NgbActiveModal,
                private fb: FormBuilder, private privateClassService: PrivateClassService,private userService: UserService, private subjectService: SubjectService) {
                 
                  if(HomeComponent.class.isEvent){
                    this.subjectService.getAllSubjectTeachers(HomeComponent.class.title).subscribe(data=> {
                      this.teachers = data;
                      this.teacher = data[0];
                    });
                  }
                  else{
                    this.subjectService.getAllSubjects().subscribe(data=> {
                      this.subjects = data;
                      console.log(data);
                    });
                  }
                  this.privateClass = new  AddPrivateClass("","","","","");
    }

    onSubmit() {
      const u = new User('-1', this.profileForm.controls.username.value,
      this.profileForm.controls.firstName.value,
      this.profileForm.controls.lastName.value,
      this.profileForm.controls.address.value,
      this.profileForm.controls.phone.value,
      this.profileForm.controls.email.value,
       this.profileForm.controls.email.value,
      this.profileForm.controls.degree.value);
      localStorage.getItem
  this.userService.editUserInformations(u).subscribe(data => {
          alert('Success');
          this.activeModal.close();
  }, error => {
      alert(error)
  });
    }
    onChangeSlider(e){
      this.privateClass.NumOfStudents = e;
     this.simpleSlider = e;
    }
    onChangeTeacher(e){
      this.teacher = e;
    }
    onchange(){
      this.privateClass.Subject = this.profileForm2.controls.subject.value;
    }
    
    secretaryDeclineClass(id:string){
      this.activeModal.close();
  
      this.privateClassService.secretaryDeclineClass(id).subscribe(data =>{
   
        alert('success');
      }, error => {
        alert(error);
      });
    }
    studentDeclineClass(id:string){
      this.activeModal.close();
  
      this.privateClassService.studentDeclineClass(id).subscribe(data =>{
   
        alert('success');
      }, error => {
        alert(error);
      });
    }
    acceptClass(id: string){
      this.activeModal.close();
  
      this.privateClassService.teacherAcceptClass(id).subscribe(data =>{
        alert('success');
      }, error => {
        alert(error);
      });
    }

    assignClass(id: string){
     
      if(this.teacher == null){
        alert('Please chose teacher.')
      }
      else{
        this.activeModal.close();
        this.privateClassService.assignClass(id,this.teacher).subscribe(data =>{
          alert('success');
        }, error => {
          alert(error);
        });
      }
    }
    teacherDeclineClass(id:string){
      this.activeModal.close();
  
      this.privateClassService.teacherDeclineClass(id).subscribe(data =>{
   
        alert('success');
      }, error => {
        alert(error);
      });
    }
    secretaryRequestClass(){
      this.activeModal.close();
      this.privateClass.Lesson = this.profileForm2.controls.lesson.value;
      this.privateClass.Date = this.name.date;
      if(this.name.time == 'no'){
        
        this.privateClass.Time = this.time.hour.toString() + ':' + this.time.minute.toString();
      }
      else{
        alert(this.name.time);
        this.privateClass.Time = this.name.time.split(':')[0] + ':' + this.name.time.split(':')[1];
      }
      this.activeModal.close();
      this.privateClassService.addPrivateClass(this.privateClass).subscribe(data =>{
        alert('success');
      }, error => {
        alert(error);
      })
    }
    requestClass(){
      this.activeModal.close();
      this.privateClass.Lesson = this.profileForm2.controls.lesson.value;
      this.privateClass.Date = this.name.date;
      if(this.name.time == 'no'){
        
        this.privateClass.Time = this.time.hour.toString() + ':' + this.time.minute.toString();
      }
      else{
        alert(this.name.time);
        this.privateClass.Time = this.name.time.split(':')[0] + ':' + this.name.time.split(':')[1];
      }
      this.activeModal.close();
      this.privateClassService.addPrivateClass(this.privateClass).subscribe(data =>{
        alert('success');
      }, error => {
        alert(error);
      })
    }
   
    joinClass(id:string){
      this.activeModal.close();
      if(Number(this.name.numOfStud) >=4) {
       
        alert('max number of students in one class is 4, please choose again');
      }
      this.privateClassService.joinClass(id).subscribe(data =>{
   
        alert('success');
      }, error => {
        alert(error);
      });
    }
    isTeacher(){
      return localStorage.getItem('group') == 'PrivatniCasoviTeachers' ? true:false;
    }
    isSecretary(){
      return localStorage.getItem('group') == 'PrivatniCasoviSecretaries' ? true:false;
    }
    isStudent(){
      return localStorage.getItem('group') == 'PrivatniCasoviStudents' ? true:false;
    }

   
}


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
    
    options: OptionsInput;
    eventsModel: Array<any>;
    @ViewChild('fullcalendar') fullcalendar: FullCalendarComponent;
    user: User;
    classes: Observable<PrivateClass>;
    focus;
    focus1;
    isEvent = false;
    static class = {
      date: " ",
      time: " ",
      startDate: " ",
      endDate: " ",
      teacher: " ",
      lesson: " ",
      numOfStud: " ",
      id: " ",
      title: " ",
      isEvent: false,
      status: " ",
      isMine: " "
    };
    notTeacherSubjects: Observable<string>;
    constructor(private userService: UserService,private subjectService: SubjectService, private privateClassService: PrivateClassService, private modalService: NgbModal) {
         this.userService.onSignIn().subscribe(data => {
            this.user = data;
            if (this.user.Username.split('_')[0] == "") {
                this.open();
            }
          console.log(HomeComponent.class);
        });
        if(this.isTeacher()){
          this.getNotTeacherSubjects();
        }

        this.getUserClasses();
    }
    getNotTeacherSubjects(){
      this.subjectService.getNotTeacherSubjects().subscribe(data =>{
        this.notTeacherSubjects = data;
      });
    }

    getUser(): User {
        return this.user;
    }

    getUserClasses(){
      this.privateClassService.getUserClasses().subscribe(data => {
        this.classes = data;
    });
    }
    open() {
      
        const modalRef = this.modalService.open(NgbdModalContent);
        modalRef.componentInstance.name = HomeComponent.class;
        modalRef.componentInstance.info = this.user.Username.split('_')[0];

        modalRef.result.then((result) => {
          this.updateEvents();
        });

        
    }
    subjectToAdd= null;
    newSubject = null;
    onchangeSubject(event){
      this.subjectToAdd = event;
    }

    onKey(event){
      this.newSubject = event.target.value;
    }

    onAddNewSubject(){
      if(this.newSubject != null){
        this.subjectService.addNewSubject(this.newSubject).subscribe(data=>{
          this.getNotTeacherSubjects();
          this.newSubject = "";
          alert('success');

        }, error => {
          alert(error);
        });
      }
    }

    onTeachSubject(){
      if(this.subjectToAdd != null){
        this.subjectService.teacherAddNewTeachingSubject(this.subjectToAdd).subscribe(data=>{
          this.getNotTeacherSubjects();
          alert('success');
        }, error => {
          alert(error);
        });
      }
    }
    isTeacherAccepted(status: string){
      if(localStorage.getItem('group') == 'PrivatniCasoviTeachers'){
        if(status == 'REQUESTED'){
          return true;
        }
      }
      return false;
    }
    isStudent(){
      return localStorage.getItem('group') == 'PrivatniCasoviStudents' ? true:false;
    }
    isTeacher(){
      if(localStorage.getItem('group') == 'PrivatniCasoviTeachers'){
       return true;
      }
      return false;
    }
    isSecretary(){
      return localStorage.getItem('group') == 'PrivatniCasoviSecretaries' ? true:false;
    }
    

    isTeacherDeleted(status: string){
      if(localStorage.getItem('group') == 'PrivatniCasoviTeachers'){
        if(status == 'ACCEPTED'){
          return true;
        }
      return false;
    }
  }
  static oldStart: Date;
  static oldEnd:Date;
    ngOnInit() {
      this.options = {
        editable: true,
        droppable: true,
        eventDragStart: function(arg){
          HomeComponent.oldStart = arg.event.start;
          HomeComponent.oldEnd = arg.event.end;
        },
        eventDrop: function(eventDrop){
          HomeComponent.eventDrop = eventDrop;
         
          let element =  document.getElementById('changeDate') as HTMLElement;
          element.click();
        },
        dateClick:function(args){
          if(localStorage.getItem('group') != 'PrivatniCasoviTeachers'){
            const date = new Date()
            date.setDate(date.getDate() - Number.parseInt("1"));
            if(args.date >= date){
              let element =  document.getElementById('radi') as HTMLElement;
          
              if(args.date.toLocaleString().split(',')[1] ==  ' 12:00:00 AM'){
                HomeComponent.class.date = args.date.toLocaleDateString();
                HomeComponent.class.time = "no";
                HomeComponent.class.isEvent = false;
              } 
              else{
                HomeComponent.class.date = args.date.toLocaleDateString();
                HomeComponent.class.time = args.date.toLocaleTimeString();
                HomeComponent.class.isEvent = false;
              }
              element.click();
            }
            else{
              alert('Date has passed.');
              
            }
          }
        },
       
        eventClick: function(arg){
           console.log( new Date());

           if(arg.event.start < new Date()){
             alert('You cant see passed event.');
           }
          if(arg.event.id.split('_')[4] != 'DECLINED' && arg.event.start > new Date()){
              let element =  document.getElementById('radi') as HTMLElement;
              HomeComponent.class.startDate = arg.event.start.toLocaleString();
              HomeComponent.class.endDate = arg.event.end.toLocaleString();
              HomeComponent.class.title = arg.event.title;
              HomeComponent.class.teacher = arg.event.id.split('_')[1];
              HomeComponent.class.numOfStud = arg.event.id.split('_')[2];
              HomeComponent.class.lesson = arg.event.id.split('_')[3];
              HomeComponent.class.id = arg.event.id.split('_')[0];
              HomeComponent.class.isEvent = true;
              HomeComponent.class.status = arg.event.id.split('_')[4]
              HomeComponent.class.isMine = arg.event.id.split('_')[5]
              console.log('more');
              element.click();
            }
            else {
              console.log('ne more');
            }

        },
        header: {
          left: 'prev next',
          center: 'title',
          right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
        },
      
        plugins: [dayGridPlugin,timeGridPlugin,listPlugin, interactionPlugin]
      };
      this.updateEvents();
  
    
   
    }
    static eventDrop: any;
    changeDate(){
      if(HomeComponent.eventDrop.event.id.split('_')[5] == 'no'){
      // let temp = Array<any>();
      // let varr:any;
      //   this.eventsModel.forEach(element => {
       
      //       if(element.id== HomeComponent.eventDrop.event.id){
              
             
      //         element.start = this.convertDateTime( HomeComponent.oldStart);
      //         element.end =  this.convertDateTime( HomeComponent.oldEnd);
      //         console.log(element.start );
      //        varr = element;
      //       }
      //       else{
      //         temp.push(element);
      //       }
           
      //   });
      //   temp.push(varr);
      //   console.log(this.eventsModel);
      //   this.eventsModel = temp;
       this.eventsModel = [{}];
      this.updateEvents();
     
        alert('You are not in this class.');
      }
      else
      {
        
        this.privateClassService.changeDate(HomeComponent.eventDrop.event.id.split('_')[0],HomeComponent.eventDrop.event.start.toLocaleString()).subscribe(data => {
          alert('success');
        }, error => {
          this.eventsModel = [{}];
          this.updateEvents();
          
      
          alert(error.split('_')[0]);
        });
      }
    }


    convertDateTime( dt:Date){
   
      let day = dt.getDate().toString();
      if (dt.getDate() < 10){
          day = "0" + day;
      }
      let month = (dt.getMonth()+1).toString();
         
     
      if (dt.getMonth()+1 < 10)
          month = "0" + month;

          if(dt.getMonth() == 12 ){
            month = '01';
          }
      return  dt.getFullYear()+'-'+month+'-'+day+'T'+dt.toTimeString().split(' ')[0];

    }


    display(){
      console.log(this.eventsModel);
    }
    eventClick(model) {
      console.log(model);
    }
    eventDragStop(model) {
      console.log(model);
    }
    
    dateClick(model) {
      console.log(model);
    }
    updateHeader() {
      this.options.header = {
        left: 'prev,next myCustomButton',
        center: 'title',
        right: ''
      };
    }
    updateEvents() {
      this.privateClassService.getUserClasses().subscribe(data => {
        console.log(data);
        this.eventsModel = [{}];
        data.forEach((value: PrivateClass) => {
          
          this.eventsModel.push({id:value.Id + '_'+ value.Teacher+'_'+value.NumberOfStudents+'_'+value.Lesson+'_'+value.Status+'_'+value.IsMine, title: value.Subject,start: value.StartDate, end: value.EndDate, color:value.Color});
      });
    });
    }
    get yearMonth(): string {
      const dateObj = new Date();
      return dateObj.getUTCFullYear() + '-' + (dateObj.getUTCMonth() + 1);
    }
}

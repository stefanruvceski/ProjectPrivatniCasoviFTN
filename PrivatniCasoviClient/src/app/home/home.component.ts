import { Component, OnInit, Input } from '@angular/core';
import { TestService } from 'app/test-service.service';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from 'app/Model/User';
import { UserService } from 'app/services/user.service';
import { PrivateClassService } from 'app/services/private-class.service';
import { Observable } from 'rxjs';
import { PrivateClass } from 'app/Model/PrivateClass';

@Component({
    selector: 'app-home',
    styleUrls: ['./home.component.scss'],
    template: `
  <div style="background: rgba(0, 0, 0, 0.6); color:white;">
    <div class="modal-header" style="background-color: rgba(0, 0, 0, 0.6);color:white;">
        <h5 class="modal-title text-center"  style="color:white;">Fill User Informations</h5>
        <button type="button" class="close" aria-label="Close" (click)="activeModal.dismiss('Cross click')">
        <span aria-hidden="true">&times;</span>
        </button>
    </div>
    <div class="modal-body" style="background-color: rgba(0, 0, 0, 0.6);color:white;">
    <div class="container-fluid">
  <div class="row no-gutter">
            <div class="col-lg-9 mx-auto">
            <form class="form-signin" [formGroup]="profileForm" (submit)="onSubmit()">
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

              <button class="btn btn-lg btn-outline-danger btn-block btn-round text-uppercase font-weight-bold mb-2" [disabled]="profileForm.invalid"  type="submit">Launch demo modal</button>

            </form>
          </div>
    </div></div></div></div>
    `
})
export class NgbdModalContent {
    user: User;
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
                private fb: FormBuilder, private userService: UserService) {
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

        this.userService.editUserInformations(u).subscribe(data => {
                alert('Success');
                this.activeModal.close();
        }, error => {
            alert('error')
        });
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
    user: User;
    classes: Observable<PrivateClass>;
    focus;
    focus1;
    constructor(private userService: UserService, private privateClassService: PrivateClassService, private modalService: NgbModal) {
         this.userService.onSignIn().subscribe(data => {
            this.user = data;
            console.log(data);
            if (this.user.Username == null) {
                this.open();
            }
        });

        this.privateClassService.getUserClasses().subscribe(data => {
            console.log(data);
            this.classes = data;
        });
        // this.service.fja().subscribe(data => alert(data));
        // this.modalService.open('Content2', { windowClass: 'dark-modal' });
    }

    getUser(): User {
        return this.user;
    }
    open() {
        const modalRef = this.modalService.open(NgbdModalContent);
        modalRef.componentInstance.name = 'World';
    }
    onClick(evenet) {
        alert((event.target as Element).id);
    }

    ngOnInit() {}
}

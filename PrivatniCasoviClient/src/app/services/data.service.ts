import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { User } from 'app/Model/User';

@Injectable()
export class DataService {

  private messageSource = new BehaviorSubject('default message');
  currentMessage = this.messageSource.asObservable();

  private userSource = new BehaviorSubject(new User("","","","","","","","","","",));
  currentUser = this.userSource.asObservable();
  constructor() { }

  changeMessage(message: string) {
    this.messageSource.next(message)
  }

  changeUser(user:User){
    this.userSource.next(user)
  }

}
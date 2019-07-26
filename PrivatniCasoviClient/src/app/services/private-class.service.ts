import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { throwError, Observable, of, Subject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { User } from 'app/Model/User';
import { PrivateClass } from 'app/Model/PrivateClass';
import { AddPrivateClass } from 'app/Model/AddPrivateClass';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json'  })
  };

@Injectable({
  providedIn: 'root'
})
export class PrivateClassService {

  constructor(private http: HttpClient) {

  }

  editUserInformations( user: User) {
    return this.http.post<User>('http://localhost:52988/', user, httpOptions).pipe(catchError(this.errorHandler));
  }
  addPrivateClass( privateClass: AddPrivateClass) {
    return this.http.post<User>('http://localhost:52988/api/privateclass/addClass', privateClass, httpOptions).pipe(catchError(this.errorHandler));
  }
  
  getUserClasses() {
    return this.http.get<Observable<PrivateClass>>('http://localhost:52988/api/privateclass/getprivateclasses').pipe(
      catchError(this.errorHandler)
      );
  }

  getClassStudents(classId:string){
    return this.http.get<Observable<string>>('http://localhost:52988/api/privateclass/getallclassstudents?classId='+classId).pipe(
      catchError(this.errorHandler)
    );
  }

  removeClassStudents(students: string, classId: string){
    return this.http.get<string>('http://localhost:52988/api/privateclass/removeclassstudents?classId='+classId+'&students='+students).pipe(
      catchError(this.errorHandler)
    );
  }

  studentDeclineClass(id: string){
    return this.http.get<string>('http://localhost:52988/api/privateclass/studentdeclineclass?classId='+id).pipe(
      catchError(this.errorHandler)
    );
  }
  teacherDeclineClass(id: string){
    return this.http.get<string>('http://localhost:52988/api/privateclass/teacherdeclineclass?classId='+id).pipe(
      catchError(this.errorHandler)
    );
  }
  
  secretaryDeclineClass(id: string){
    return this.http.get<string>('http://localhost:52988/api/privateclass/secretarydeclineclass?classId='+id).pipe(
      catchError(this.errorHandler)
    );
  }
  joinClass(id: string) {
    return this.http.get<string>('http://localhost:52988/api/privateclass/joinclass?classId='+id).pipe(
        catchError(this.errorHandler)
      );
  }
  
  changeDate(id: string,date: string){
    
    return this.http.get<string>('http://localhost:52988/api/privateclass/changedate?classId='+id+'&date='+date).pipe(
      catchError(this.errorHandler)
    );
  }
  
  
  teacherAcceptClass(id: string){
    return this.http.get<string>('http://localhost:52988/api/privateclass/acceptClass?id='+id).pipe(
        catchError(this.errorHandler)
      );
  }
  assignClass(id: string,teacher: string){
    return this.http.get<string>('http://localhost:52988/api/privateclass/assignClass?classId='+id+'&teacher='+teacher).pipe(
        catchError(this.errorHandler)
      );
  }
  

  errorHandler(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred: ', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}` + `body was: ${error.error.error_description}`
      );
    }
    return throwError(error.error.Message);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }
}

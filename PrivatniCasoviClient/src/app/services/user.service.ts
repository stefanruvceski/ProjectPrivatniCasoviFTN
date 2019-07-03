import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { throwError, Observable, of, Subject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { User } from 'app/Model/User';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json'  })
  };

@Injectable({
  providedIn: 'root'
})
export class UserService {
    username: Observable<string>;
    private userSubject: Subject<string>;
  constructor(private http: HttpClient) {
    this.userSubject = new Subject<string>();
    this.username = this.userSubject.asObservable();
  }

  editUserInformations( user: User) {
    this.username = Observable.create(user.Username);
    this.userSubject.next(user.Username);
    return this.http.post<User>('http://localhost:52988/api/users/edit', user, httpOptions).pipe(catchError(this.errorHandler));
  }
  getUserInfo() {
    return this.http.get<User>('http://localhost:52988/api/users/getuserinfo').pipe(
        catchError(this.handleError<User>(`getuser `))
      );
  }
  onSignIn() {
    let str = '';
    this.http.get<User>('http://localhost:52988/api/users/onsignin').subscribe(data => {
       str = data.Username;
     this.username = Observable.create(str);
     this.userSubject.next(str);
     });

    return this.http.get<User>('http://localhost:52988/api/users/onsignin').pipe(
        catchError(this.handleError<User>(`getuser `))
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

    return throwError('Something bad happend, please try again later...');
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }
}

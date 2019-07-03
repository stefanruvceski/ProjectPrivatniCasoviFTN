import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { throwError, Observable, of, Subject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { User } from 'app/Model/User';
import { PrivateClass } from 'app/Model/PrivateClass';

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
  getUserClasses() {
    return this.http.get<Observable<PrivateClass>>('http://localhost:52988/api/privateclass/getprivateclasses').pipe(
        catchError(this.handleError<Observable<PrivateClass>>(`getuser `))
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

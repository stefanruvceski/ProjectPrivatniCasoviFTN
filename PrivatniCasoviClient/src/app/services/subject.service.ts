import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { Observable, throwError, of } from 'rxjs';
import { HttpErrorResponse, HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {


  constructor(private http: HttpClient) { }

  getAllSubjects(){
    return this.http.get<Observable<string>>('http://localhost:52988/api/subject/getall').pipe(
        catchError(this.handleError<Observable<string>>(`acceptClass `))
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

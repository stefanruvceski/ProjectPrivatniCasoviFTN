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
        catchError(this.errorHandler)
      );
  }

  getAllSubjectTeachers(subject:string){
    return this.http.get<Observable<string>>('http://localhost:52988/api/subject/getsubjectteachers?subject='+subject).pipe(
        catchError(this.errorHandler)
      );
  }
  
  addNewSubject(subject: string){
    return this.http.get<Observable<string>>('http://localhost:52988/api/subject/addnewsubject?subject='+subject).pipe(
      catchError(this.errorHandler)
    );
  }

  teacherAddNewTeachingSubject(subject: string){
    return this.http.get<Observable<string>>('http://localhost:52988/api/subject/teacheraddnewteachingsubject?subject='+subject).pipe(
      catchError(this.errorHandler)
    );
  }

  getNotTeacherSubjects(){
    return this.http.get<Observable<string>>('http://localhost:52988/api/subject/getnotteachersubjects').pipe(
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

import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const jwt = localStorage.getItem('jwt');
        console.log(req);
        console.log('usao inter');
        if (jwt) {
            req = req.clone({
                setHeaders: {
                    Authorization: 'Bearer ' + jwt,
                     'Access-Control-Allow-Origin': '*',

'Access-Control-Allow-Methods': 'HEAD, GET, POST, PUT, PATCH, DELETE',
'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token'
             } });
        }
        console.log(req);

        return next.handle(req);
    }
}

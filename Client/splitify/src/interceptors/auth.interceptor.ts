import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { UserService } from 'src/core/services/user.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(
      private userService: UserService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
        const token = localStorage.getItem('AUTH_TOKEN');     

        if (token){
            request = request.clone({
                setHeaders: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
                }
            });
        }

        return next.handle(request).pipe(
            catchError((error: HttpErrorResponse) => {
              if (error.status === 401) {
                console.log('401 RESPONSE')
                this.userService.userUnauthorized.emit();
              }
              else if(error.status === 403){
                this.userService.userNotVerified.emit();
              }
              return throwError(() => error);
            })
          );
    }
}
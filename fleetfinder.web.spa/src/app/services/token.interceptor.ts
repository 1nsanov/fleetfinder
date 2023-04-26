import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';
import {catchError, Observable, switchMap, throwError} from 'rxjs';
import {IdentifyApiService} from "../api/Identify/identify.api.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private identifyService: IdentifyApiService) {}
  private refreshTokenInProgress = false;
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
    if (this.identifyService.isAuthenticated() && !this.refreshTokenInProgress) {
      const tokenExpiration = this.identifyService.getTokenExpiration(); //TODO: Написать апи метод  
      const now = new Date().getTime();
      if (tokenExpiration && tokenExpiration < now) {
        if (!this.refreshTokenInProgress) {
          this.refreshTokenInProgress = true;
          return this.identifyService.refreshToken().pipe(
            catchError((error) => {
              if (error instanceof HttpErrorResponse && (error.status === 401 || error.status === 500))
                this.identifyService.logout().subscribe();

              return throwError(error);
            }),
            switchMap((result) => {
              this.refreshTokenInProgress = false;
              const authRequest = request.clone({
                headers: request.headers.set('Authorization', `Bearer ${result.Token.Access}`)
              });
              return next.handle(authRequest);
            })
          );
        } else {
          // token is still valid, add it to the request headers
          const authRequest = request.clone({
            headers: request.headers.set('Authorization', `Bearer ${this.identifyService.getAccessToken()}`)
          });
          return next.handle(authRequest);
        }
      } else {
        const authRequest = request.clone({
          headers: request.headers.set('Authorization', `Bearer ${this.identifyService.getAccessToken()}`)
        });
        return next.handle(authRequest);
      }
    }
    else {
      return next.handle(request);
    }
  }
}

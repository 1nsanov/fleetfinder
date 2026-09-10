import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import {catchError, Observable, switchMap, throwError} from 'rxjs';
import {IdentityApiService} from "../api/Identity/identity.api.service";
import {NotificationService} from "../services/notification.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private identityService: IdentityApiService,
              private notification: NotificationService) {}
  private refreshTokenInProgress = false;
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
    if (this.identityService.isAuthenticated() && !this.refreshTokenInProgress) {
      const tokenExpiration = this.identityService.getTokenExpiration();
      const now = new Date().getTime();
      if (tokenExpiration && tokenExpiration < now) {
        this.refreshTokenInProgress = true;
        return this.identityService.refreshToken().pipe(
          catchError((error) => {
            if (error instanceof HttpErrorResponse){
              this.notification.error("Токен авторизиции истек")
              this.identityService.logout().subscribe();
            }
            return throwError(error);
          }),
          switchMap((result) => {
            this.refreshTokenInProgress = false;
            const authRequest = request.clone({
              headers: request.headers
                .set('Authorization', `Bearer ${result.Token.Access}`)
            });
            return next.handle(authRequest);
          })
        );
      } else {
        const authRequest = request.clone({
          headers: request.headers
            .set('Authorization', `Bearer ${this.identityService.getAccessToken()}`)
        });
        return next.handle(authRequest);
      }
    }
    else {
      return next.handle(request);
    }
  }
}

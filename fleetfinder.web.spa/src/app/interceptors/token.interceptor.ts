import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';
import {catchError, Observable, switchMap, throwError} from 'rxjs';
import {IdentifyApiService} from "../api/Identify/identify.api.service";
import {NotificationService} from "../services/notification.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private identifyService: IdentifyApiService,
              private notification: NotificationService) {}
  private refreshTokenInProgress = false;
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
    if (this.identifyService.isAuthenticated() && !this.refreshTokenInProgress) {
      const tokenExpiration = this.identifyService.getTokenExpiration();
      const now = new Date().getTime();
      if (tokenExpiration && tokenExpiration < now) {
        this.refreshTokenInProgress = true;
        return this.identifyService.refreshToken().pipe(
          catchError((error) => {
            if (error instanceof HttpErrorResponse){
              this.notification.error("Токен авторизиции истек")
              this.identifyService.logout().subscribe();
            }
            return throwError(error);
          }),
          switchMap((result) => {
            this.refreshTokenInProgress = false;
            console.log("switchMap")
            const authRequest = request.clone({
              headers: request.headers
                .set('Authorization', `Bearer ${result.Token.Access}`)
                .set('UserId', this.identifyService.claims?.Id?.toString() ?? '')
            });
            return next.handle(authRequest);
          })
        );
      } else {
        const authRequest = request.clone({
          headers: request.headers
            .set('Authorization', `Bearer ${this.identifyService.getAccessToken()}`)
            .set('UserId', this.identifyService.claims?.Id?.toString() ?? '')
        });
        return next.handle(authRequest);
      }
    }
    else {
      return next.handle(request);
    }
  }
}

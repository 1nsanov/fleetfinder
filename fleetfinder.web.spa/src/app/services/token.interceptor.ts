import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {Observable, switchMap} from 'rxjs';
import {IdentifyApiService} from "../api/Identify/identify.api.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private identifyService: IdentifyApiService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.identifyService.isAuthenticated()) {
      const tokenExpiration = this.identifyService.getTokenExpiration();
      const now = new Date().getTime();
      if (tokenExpiration && tokenExpiration < now) {
        // token has expired, refresh it
        return this.identifyService.refreshToken().pipe(
          switchMap((newToken) => {
            const authRequest = request.clone({
              headers: request.headers.set('Authorization', `Bearer ${newToken.Token.Access}`)
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
      return next.handle(request);
    }
  }
}

import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import {Observable, tap} from 'rxjs';
import {Router} from "@angular/router";
import {namesRoute} from "../data/names-route";
import {IdentityApiService} from "../api/Identity/identity.api.service";

const CREDENTIAL_PATHS = ['/identity/sign-in', '/identity/sign-up'];

@Injectable()
export class RedirectInterceptor implements HttpInterceptor {

  constructor(private router: Router,
              private identityService: IdentityApiService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      tap({
        error: (error) => {
          if (error.status === 401 && !this.isCredentialRequest(request)) {
            this.identityService.writeToken(null);
            this.router.navigate([`/${namesRoute.HOME}`]).then(() => window.location.reload());
          }
        }
      })
    );
  }

  private isCredentialRequest(request: HttpRequest<unknown>): boolean {
    const url = request.url.toLowerCase();
    return CREDENTIAL_PATHS.some(path => url.includes(path));
  }
}

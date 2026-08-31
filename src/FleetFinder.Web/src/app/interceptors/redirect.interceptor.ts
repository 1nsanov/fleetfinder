import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import {Observable, tap} from 'rxjs';
import {Router} from "@angular/router";
import {namesRoute} from "../data/names-route";
import {IdentityApiService} from "../api/Identity/identity.api.service";

@Injectable()
export class RedirectInterceptor implements HttpInterceptor {

  constructor(private router: Router,
              private identifyService: IdentityApiService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      tap(
        event => {},
        error => {
          if (error.status === 401) {
            this.identifyService.writeToken(null);
            this.router.navigate([`/${namesRoute.HOME}`]).then(() => window.location.reload());
          }
        }
      )
    );
  }
}

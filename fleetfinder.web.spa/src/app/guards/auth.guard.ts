import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import {IdentifyApiService} from "../api/Identify/identify.api.service";
import {namesRoute} from "../data/names-route";
import {NotificationService} from "../services/notification.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private identifyService: IdentifyApiService,
              private router: Router,
              private notification: NotificationService) {
  }

  canActivate(): boolean {
    if (!this.identifyService.isAuthenticated()){
      this.router.navigate([`/${namesRoute.signIn}`])
        .then(() => this.notification.notify('Для доступа к этой странице необходимо выполнить вход в систему.'))
    }

    return this.identifyService.isAuthenticated();
  }
}

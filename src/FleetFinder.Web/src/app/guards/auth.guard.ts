import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {IdentityApiService} from "../api/Identity/identity.api.service";
import {namesRoute} from "../data/names-route";
import {NotificationService} from "../services/notification.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {
  constructor(private identityService: IdentityApiService,
              private router: Router,
              private notification: NotificationService) {
  }

  canActivate(): boolean {
    if (!this.identityService.isAuthenticated()){
      this.router.navigate([`/${namesRoute.SIGN_IN}`])
        .then(() => this.notification.notify('Для доступа к этой странице необходимо выполнить вход в систему.'))
    }

    return this.identityService.isAuthenticated();
  }
}

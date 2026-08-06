import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {IdentifyApiService} from "../api/Identify/identify.api.service";
import {namesRoute} from "../data/names-route";
import {NotificationService} from "../services/notification.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {
  constructor(private identifyService: IdentifyApiService,
              private router: Router,
              private notification: NotificationService) {
  }

  canActivate(): boolean {
    if (!this.identifyService.isAuthenticated()){
      this.router.navigate([`/${namesRoute.SIGN_IN}`])
        .then(() => this.notification.notify('Для доступа к этой странице необходимо выполнить вход в систему.'))
    }

    return this.identifyService.isAuthenticated();
  }
}

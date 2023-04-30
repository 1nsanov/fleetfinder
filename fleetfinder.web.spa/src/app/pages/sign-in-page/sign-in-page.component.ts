import { Component } from '@angular/core';
import {SignInModel} from "../../models/user/sign-in.model";
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {ISignInRequest} from "../../api/Identify/identify.api.models";
import {Router} from "@angular/router";
import {namesRoute} from "../../data/names-route";
import {tap} from "rxjs";
import {NotificationService} from "../../services/notification.service";

@Component({
  selector: 'app-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss']
})
export class SignInPageComponent {
  constructor(private identifyService: IdentifyApiService,
              private router: Router,
              private notification: NotificationService) {
  }
  user: SignInModel = new SignInModel();
  isLoad = false;
  signIn(){
    this.isLoad = true;
    const request = this.user as ISignInRequest;
    this.identifyService.signIn(request).subscribe(() => {
      this.isLoad = false;
      this.router.navigate([`/${namesRoute.home}`]).then(() => window.location.reload())
    }, error => {
      this.isLoad = false
      this.notification.error("Не верный логин и/или пароль.")
    });
  }
}

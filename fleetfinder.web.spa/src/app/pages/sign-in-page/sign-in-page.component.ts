import { Component } from '@angular/core';
import {SignInModel} from "../../models/user/sign-in.model";
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {ISignInRequest} from "../../api/Identify/identify.api.models";
import {Router} from "@angular/router";
import {namesRoute} from "../../models/names-route";
import {tap} from "rxjs";

@Component({
  selector: 'app-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss']
})
export class SignInPageComponent {
  constructor(private identifyService: IdentifyApiService,
              private router: Router) {
  }
  user: SignInModel = new SignInModel();
  signIn(){
    const request = this.user as ISignInRequest;
    this.identifyService.signIn(request).subscribe(res => {
      console.log(res)
      this.router.navigate([`/${namesRoute.home}`])
    });
  }
}

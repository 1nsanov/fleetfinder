import { Component } from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {SignUpModel} from "../../models/user/sign-up.model";
import {ISignUpRequest} from "../../api/Identify/identify.api.models";
import {NotificationService} from "../../services/notification.service";
import {Router} from "@angular/router";
import {namesRoute} from "../../data/names-route";
import {HttpErrorResponse} from "@angular/common/http";
import {catchError, throwError} from "rxjs";

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent {
  step: number = 1;
  user: SignUpModel = new SignUpModel();
  isLoadSignUp = false;

  constructor(private identifyService: IdentifyApiService,
              private notification: NotificationService,
              private router: Router) {
  }
  nextStep() {
    if (this.validationStepOne())
      this.step++;
    else
      this.notification.error("Заполните ФИО!")
  }

  previousStep() {
    this.step--;
  }

  validationStepOne() : boolean {
    return !!this.user.FullName.First && !!this.user.FullName.Second;
  }

  signUp(){
    if (this.user.Password !== this.user.RepeatPassword){
      this.notification.error("Пароли не совпадают!");
      return;
    }
    this.isLoadSignUp = true;
    const request = this.user as ISignUpRequest;
    this.identifyService.signUp(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessages = error.error.errors;
        const errorArray = Object.values(errorMessages).flat();
        const errorString = errorArray.join('<br>');
        this.notification.error(errorString);
        this.isLoadSignUp = false;
        return throwError(error);
      })
    ).subscribe(() => {
      this.router.navigate([`/${namesRoute.home}`]).then(() => window.location.reload());
    });
  }
}

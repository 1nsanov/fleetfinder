import { Component } from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {SignUpModel} from "../../models/user/sign-up.model";
import {ISignUpRequest} from "../../api/Identify/identify.api.models";
import {NotificationService} from "../../services/notification.service";
import {Router} from "@angular/router";
import {namesRoute} from "../../models/names-route";
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

  constructor(private identifyService: IdentifyApiService,
              private notification: NotificationService,
              private router: Router) {
  }
  nextStep() {
    if (this.validationStepOne())
      this.step++;
    else
      this.notification.notify("Заполните ФИО!")
  }

  previousStep() {
    this.step--;
  }

  validationStepOne() : boolean {
    return !!this.user.FullName.First && !!this.user.FullName.Second;
  }

  signUp(){
    if (this.user.Password !== this.user.RepeatPassword){
      this.notification.notify("Пароли не совпадают!");
      return;
    }
    const request = this.user as ISignUpRequest;
    this.identifyService.signUp(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessages = error.error.errors;
        const errorArray = Object.values(errorMessages).flat();
        const errorString = errorArray.join('\n');
        this.notification.notify(errorString);
        return throwError(error);
      })
    ).subscribe(() => {
      this.router.navigate([`/${namesRoute.home}`]).then(() => window.location.reload());
    });
  }
}

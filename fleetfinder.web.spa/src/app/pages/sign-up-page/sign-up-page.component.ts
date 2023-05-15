import { Component } from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {SignUpModel} from "../../models/interfaces/user/sign-up.model";
import {ISignUpRequest} from "../../api/Identify/identify.api.models";
import {NotificationService} from "../../services/notification.service";
import {Router} from "@angular/router";
import {namesRoute} from "../../data/names-route";
import {HttpErrorResponse} from "@angular/common/http";
import {catchError, throwError} from "rxjs";
import {TimeoutService} from "../../services/timeout.service";

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent {
  step: number = 1;
  user: SignUpModel = new SignUpModel();
  isLoadSignUp = false;
  errors = {
    FirstName: "",
    SecondName: "",
    Surname: "",
  }

  constructor(private identifyService: IdentifyApiService,
              private notification: NotificationService,
              private router: Router,
              private timeoutService: TimeoutService) {
  }
  async nextStep() {
    await this.validationStepOne()
    if (Object.values(this.errors).join("")) {
      this.notification.error("Заполните ФИО!")
    } else {
      this.step++;
    }
  }

  previousStep() {
    this.step--;
  }

  async validationStepOne() {
    this.clearErrors()
    await this.timeoutService.wait(100);
    if (!this.user.FullName.First)
      this.errors.FirstName = "Поле обязательно к заполнению";
    if (!this.user.FullName.Second)
      this.errors.SecondName = "Поле обязательно к заполнению";
    if (!this.user.FullName.Surname)
      this.errors.Surname = "Поле обязательно к заполнению";
  }

  clearErrors(){
    this.errors.FirstName = ""
    this.errors.SecondName = ""
    this.errors.Surname = ""
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

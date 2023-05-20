import {Component, OnInit} from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {FullName, SignUpModel} from "../../models/interfaces/user/sign-up.model";
import {ISignUpRequest} from "../../api/Identify/identify.api.models";
import {NotificationService} from "../../services/notification.service";
import {Router} from "@angular/router";
import {namesRoute} from "../../data/names-route";
import {HttpErrorResponse} from "@angular/common/http";
import {catchError, throwError} from "rxjs";
import {TimeoutService} from "../../services/timeout.service";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent implements OnInit{
  step: number = 1;
  form: FormGroup<SignUpModel>
  isLoad = false;
  errors = {
    FirstName: "",
    SecondName: "",
    Surname: "",
  }

  constructor(private identifyService: IdentifyApiService,
              private notification: NotificationService,
              private router: Router,
              private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.initFormBuilder();
  }

  async nextStep() {
    this.formFullNameMarkAsTouched();
    if (this.fullNameGroup.valid)
      this.step++;
  }

  previousStep() {
    this.step--;
  }

  signUp(){
    if (this.form.value.Password !== this.form.value.RepeatPassword){
      this.notification.error("Пароли не совпадают!");
      return;
    }

    this.formMarkAsTouched()
    if (this.form.valid){
      this.isLoad = true;
      const request = this.form.value as ISignUpRequest;
      this.identifyService.signUp(request).pipe(
        catchError((error: HttpErrorResponse) => {
          this.isLoad = false;
          this.notification.errorFromHttp(error);
          return throwError(error);
        })
      ).subscribe(() => {
        this.router.navigate([`/${namesRoute.HOME}`]).then(() => window.location.reload());
      });
    }
  }

  initFormBuilder(){
    this.form = this.formBuilder.group<SignUpModel>({
      Login: new FormControl<string| null>('', Validators.required),
      Password: new FormControl<string| null>('', Validators.required),
      RepeatPassword: new FormControl<string| null>('', Validators.required),
      Email: new FormControl<string| null>('', Validators.required),
      Organization: new FormControl<string| null>(''),
      FullName: this.formBuilder.group<FullName>({
        First: new FormControl<string | null>('', Validators.required),
        Second: new FormControl<string | null>('', Validators.required),
        Surname: new FormControl<string | null>(''),
      }),
    });
  }

  formMarkAsTouched(){
    Object.values(this.form.controls).forEach(control => {
      control.markAsTouched();
    });
  }

  fullNameGroup: FormGroup;
  formFullNameMarkAsTouched(){
    this.fullNameGroup = this.form.get('FullName') as FormGroup;
    if (this.fullNameGroup) {
      const fullNameControls = this.fullNameGroup.controls;
      Object.values(fullNameControls).forEach(control => {
        control.markAsTouched();
      });
    }
  }
}

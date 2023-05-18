import {Component, OnInit} from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {ISignInRequest} from "../../api/Identify/identify.api.models";
import {Router} from "@angular/router";
import {namesRoute} from "../../data/names-route";
import {NotificationService} from "../../services/notification.service";
import {SignInModel} from "../../models/interfaces/user/sign-in.model";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.scss']
})
export class SignInPageComponent implements OnInit{
  form: FormGroup<SignInModel>;
  isLoad = false;

  constructor(private identifyService: IdentifyApiService,
              private router: Router,
              private notification: NotificationService,
              private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.initFormBuilder();
  }

  signIn(){
    this.formMarkAsTouched();
    if (this.form.valid){
      this.isLoad = true;
      const request = this.form.value as ISignInRequest;
      this.identifyService.signIn(request).subscribe(() => {
        this.isLoad = false;
        this.router.navigate([`/${namesRoute.HOME}`]).then(() => window.location.reload())
      }, error => {
        this.isLoad = false
        this.notification.error("Не верный логин и/или пароль.")
      });
    }
  }

  initFormBuilder(){
    this.form = this.formBuilder.group<SignInModel>({
      Login: new FormControl<string | null>('', Validators.required),
      Password: new FormControl<string | null>('', Validators.required),
    })
  }

  formMarkAsTouched() {
    Object.values(this.form.controls).forEach(control => {
      control.markAsTouched();
    });
  }
}

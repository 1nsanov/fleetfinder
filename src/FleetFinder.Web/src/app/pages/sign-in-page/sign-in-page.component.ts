import {Component, OnInit, ChangeDetectionStrategy} from '@angular/core';
import {IdentityApiService} from "../../api/Identity/identity.api.service";
import {ISignInRequest} from "../../api/Identity/identity.api.models";
import {Router} from "@angular/router";
import {namesRoute} from "../../data/names-route";
import {NotificationService} from "../../services/notification.service";
import {SignInModel} from "../../models/interfaces/user/sign-in.model";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {TimeoutService} from "../../services/timeout.service";

@Component({
    selector: 'app-sign-in-page',
    templateUrl: './sign-in-page.component.html',
    styleUrls: ['./sign-in-page.component.scss'],
    changeDetection: ChangeDetectionStrategy.Eager,
    standalone: false
})
export class SignInPageComponent implements OnInit{
  form: FormGroup<SignInModel>;
  isLoad = false;

  constructor(private identityService: IdentityApiService,
              private router: Router,
              private notification: NotificationService,
              private formBuilder: FormBuilder,
              private timeoutService: TimeoutService) {
  }

  ngOnInit(): void {
    this.initFormBuilder();
  }

  signIn(){
    this.formMarkAsTouched();
    if (this.form.valid){
      this.isLoad = true;
      const request = this.form.value as ISignInRequest;
      this.identityService.signIn(request).subscribe(async () => {
        await this.timeoutService.wait(100);
        this.isLoad = false;
        this.router.navigate([`/${namesRoute.HOME}`]).then(() => {
          window.location.reload()
        })
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

import { Component } from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {SignUpModel} from "../../models/user/sign-up.model";
import {ISignUpRequest} from "../../api/Identify/identify.api.models";

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.scss']
})
export class SignUpPageComponent {
  step: number = 1;
  user: SignUpModel = new SignUpModel();

  constructor(private identifyApiService: IdentifyApiService) {
  }
  nextStep() {
    this.step++;
  }

  previousStep() {
    this.step--;
  }

  signUp(){
    const request = this.user as ISignUpRequest;
    console.log(request)
  }
}

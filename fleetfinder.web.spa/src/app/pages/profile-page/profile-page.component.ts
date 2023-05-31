import { Component } from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent {
  isEdit: boolean = false;
  isActiveChangePassword: boolean = false;

  constructor(public identifyService: IdentifyApiService) {
  }

  saveProfile(){

  }

  cancelProfile(){
    this.isEdit = false
  }

  changePassword(){

  }
}

import {Component, OnInit} from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {UserProfileApiService} from "../../api/UserProfile/user-profile.api.service";
import {ImageApiService} from "../../api/Image/image.api.service";
import {Contact} from "../../api/Common/Contact";
import {reportUnhandledError} from "rxjs/internal/util/reportUnhandledError";
import {UserProfileGetResponse} from "../../api/UserProfile/get.model";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ProfileForm} from "../../models/interfaces/user/profile/profile-form.model";
import {FullNameForm} from "../../models/interfaces/user/sign-up.model";
import {ContactForm} from "../../models/interfaces/user/profile/contact-form.model";

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent implements OnInit{
  disableForm: boolean = true;
  isLoadProfile: boolean = false;
  isActiveChangePassword: boolean = false;
  contact: Contact;
  login: string;
  copyResponse: UserProfileGetResponse;
  profileForm: FormGroup<ProfileForm>

  constructor(public identifyService: IdentifyApiService,
              private userProfileService: UserProfileApiService,
              private imageService: ImageApiService,
              private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.getUser();
  }

  getUser(){
    this.isLoadProfile = true;
    this.userProfileService.get().subscribe((res) => {
      this.copyResponse = res;
      this.contact = res.Contact as Contact;
      this.contact.ImageUrl = res.ImageUrl;
      this.contact.Title = res.Organization ?? `${res.FullName.First} ${res.FullName.Second} ${res.FullName.Surname}`
      this.login = res.Login;
      this.initFormProfileBuilder(res);
      this.isLoadProfile = false;
    })
  }

  initFormProfileBuilder(item: UserProfileGetResponse) {
    this.profileForm = this.formBuilder.group<ProfileForm>({
      Email: new FormControl<string | null>(item?.Email ?? null, Validators.required),
      Organization: new FormControl<string | null>(item?.Organization ?? null),
      FullName: this.formBuilder.group<FullNameForm>({
        First: new FormControl<string | null>(item.FullName.First, Validators.required),
        Second: new FormControl<string | null>(item.FullName.Second, Validators.required),
        Surname: new FormControl<string | null>(item.FullName?.Surname ?? null),
      }),
      Contact: this.formBuilder.group<ContactForm>({
        PhoneViber: new FormControl<string | null>(item.Contact?.PhoneViber ?? null),
        PhoneTelegram: new FormControl<string | null>(item.Contact?.PhoneTelegram ?? null),
        PhoneWhatsapp: new FormControl<string | null>(item.Contact?.PhoneWhatsapp ?? null),
        WorkingMode: new FormControl<string | null>(item.Contact?.WorkingMode ?? null),
      }),
    })
  }

  formMarkAsTouched(){
    Object.values(this.profileForm.controls).forEach(control => {
      control.markAsTouched();
    });
    this.formFullNameMarkAsTouched();
  }

  fullNameGroup: FormGroup;
  formFullNameMarkAsTouched(){
    this.fullNameGroup = this.profileForm.get('FullName') as FormGroup;
    if (this.fullNameGroup) {
      const fullNameControls = this.fullNameGroup.controls;
      Object.values(fullNameControls).forEach(control => {
        control.markAsTouched();
      });
    }
  }

  saveProfile(){
    this.formMarkAsTouched();
    if (this.profileForm.valid && this.fullNameGroup.valid){
      this.disableForm = true;
    }
  }

  cancelProfile(){
    this.initFormProfileBuilder(this.copyResponse);
    this.disableForm = true;
  }

  changePassword(){

  }
}

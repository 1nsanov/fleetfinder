import {Component, OnInit} from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {UserProfileApiService} from "../../api/UserProfile/user-profile.api.service";
import {ImageApiService} from "../../api/Image/image.api.service";
import {Contact} from "../../api/Common/Contact";
import {UserProfileGetResponse} from "../../api/UserProfile/get.model";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ProfileForm} from "../../models/interfaces/user/profile/profile-form.model";
import {FullNameForm} from "../../models/interfaces/user/sign-up.model";
import {ContactForm} from "../../models/interfaces/user/profile/contact-form.model";
import {ImagePostRequest} from "../../api/Image/post.models";
import {FirebaseStorageFolder} from "../../models/enums/common/firebase-storage-folder.enum";
import {UserProfilePutRequest} from "../../api/UserProfile/put.model";
import {catchError, throwError} from "rxjs";
import {HttpErrorResponse} from "@angular/common/http";
import {NotificationService} from "../../services/notification.service";
import {ImageDeleteRequest} from "../../api/Image/delete.models";
import {ChangePasswordForm} from "../../models/interfaces/user/profile/change-password-form.model";
import {UserProfilePutPassword} from "../../api/UserProfile/put-password.model";

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
  fullNameGroup: FormGroup;
  contactGroup: FormGroup;
  changePasswordForm: FormGroup<ChangePasswordForm>;
  previewImage: string | null = null;
  requestImagePost : ImagePostRequest = {
    Folder: FirebaseStorageFolder.UserProfile,
    Files: []
  }
  requestImageDelete : ImageDeleteRequest = {
    Folder: FirebaseStorageFolder.UserProfile,
    Urls: []
  }
  isLoadSave = false;
  isLoadChangePassword = false;

  constructor(public identifyService: IdentifyApiService,
              private userProfileService: UserProfileApiService,
              private imageService: ImageApiService,
              private formBuilder: FormBuilder,
              private notification: NotificationService) {
  }

  ngOnInit(): void {
    this.getUser();
    this.initFormChangePasswordBuilder();
  }

  getUser(){
    this.isLoadProfile = true;
    this.userProfileService.get().subscribe((res) => {
      this.copyResponse = res;
      this.contact = res.Contact as Contact;
      this.contact.ImageUrl = res.ImageUrl;
      this.contact.Title = !!res.Organization ? res.Organization : `${res.FullName.First} ${res.FullName.Second} ${res.FullName.Surname ?? ''}`
      this.login = res.Login;
      this.previewImage = res.ImageUrl;
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
        PhoneViber: new FormControl<string | null>(item.Contact?.PhoneViber ?? null, [Validators.minLength(8), Validators.maxLength(8)]),
        PhoneTelegram: new FormControl<string | null>(item.Contact?.PhoneTelegram ?? null, [Validators.minLength(8), Validators.maxLength(8)]),
        PhoneWhatsapp: new FormControl<string | null>(item.Contact?.PhoneWhatsapp ?? null, [Validators.minLength(8), Validators.maxLength(8)]),
        WorkingMode: new FormControl<string | null>(item.Contact?.WorkingMode ?? null),
      }),
    })
  }
  formMarkAsTouched(){
    Object.values(this.profileForm.controls).forEach(control => {
      control.markAsTouched();
    });
    this.formFullNameMarkAsTouched();
    this.formContactMarkAsTouched();
  }
  formFullNameMarkAsTouched(){
    this.fullNameGroup = this.profileForm.get('FullName') as FormGroup;
    if (this.fullNameGroup) {
      const controls = this.fullNameGroup.controls;
      Object.values(controls).forEach(control => {
        control.markAsTouched();
      });
    }
  }
  formContactMarkAsTouched(){
    this.contactGroup = this.profileForm.get('Contact') as FormGroup;
    if (this.contactGroup) {
      const controls = this.contactGroup.controls;
      Object.values(controls).forEach(control => {
        control.markAsTouched();
      });
    }
  }

  initFormChangePasswordBuilder(){
    this.changePasswordForm = this.formBuilder.group<ChangePasswordForm>({
      CurrentPassword: new FormControl<string | null>(null, Validators.required),
      NewPassword: new FormControl<string | null>(null, Validators.required),
    });
  }

  formChangePasswordNarkAsTouched(){
    Object.values(this.changePasswordForm.controls).forEach(control => {
      control.markAsTouched();
    });
  }


  async saveProfile(){
    this.formMarkAsTouched();
    if (this.profileForm.valid && this.fullNameGroup.valid && this.contactGroup.valid) {
      this.disableForm = false;
      this.isLoadSave = true;
      this.imageService.delete(this.requestImageDelete).subscribe( async () => {
        if (this.previewImage?.includes("firebase"))
          this.requestImagePost.Files = [];
        await this.imageService.upload(this.requestImagePost).then((res) => {
          let request = this.profileForm.value as UserProfilePutRequest;
          if (res.length > 0)
            request.ImageUrl = this.previewImage = res[0];
          else
            request.ImageUrl = this.previewImage;
          this.userProfileService.put(request).pipe(
            catchError((error: HttpErrorResponse) => {
              this.isLoadSave = false;
              this.notification.errorFromHttp(error);
              return throwError(error);
            })
          ).subscribe(() => {
            this.identifyService.getClaims().subscribe();
            this.contact = request.Contact as Contact;
            this.contact.Title = !!request.Organization ? request.Organization : `${request.FullName.First} ${request.FullName.Second} ${request.FullName.Surname ?? ''}`
            this.contact.ImageUrl = this.previewImage;
            this.disableForm = true;
            this.isLoadSave = false;
            this.notification.notify('Данные успешно обновлены')
          })
        })
      })
    }
  }

  resetChangeProfile() {
    this.initFormProfileBuilder(this.copyResponse);
    this.previewImage = this.copyResponse.ImageUrl;
    this.disableForm = true;
  }

  changePassword(){
    this.formChangePasswordNarkAsTouched();
    if (this.changePasswordForm.valid) {
      this.resetChangeProfile();
      this.isLoadChangePassword = true;
      const request = this.changePasswordForm.value as UserProfilePutPassword;
      this.userProfileService.changePassword(request).pipe(
        catchError((error: HttpErrorResponse) => {
          this.isLoadChangePassword = false;
          this.initFormChangePasswordBuilder();
          if (error.error instanceof Object)
            this.notification.errorFromHttp(error);
          else {
            const message = error.error.toString().split(":")[1].split(".")[0];
            this.notification.error(message);
          }
          return throwError(error);
        })
      ).subscribe(() => {
        this.initFormChangePasswordBuilder();
        this.isLoadChangePassword = false;
        this.notification.notify('Пароль успешно обновлен')
      })
    }
  }

  switchVisibleChangePassword() {
    if (!this.isLoadChangePassword)
      this.isActiveChangePassword = !this.isActiveChangePassword;
  }

  onSelectImage(file: File | null) {
    if (!file) return;
    this.requestImagePost.Files = [file];
    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.initImageRequestDelete();
      this.previewImage = e.target.result;
    };
    reader.readAsDataURL(file);
  }

  onRemoveImage() {
    this.initImageRequestDelete();
    this.previewImage = null;
  }

  initImageRequestDelete(){
    if (this.previewImage && this.previewImage.includes("firebase"))
      this.requestImageDelete.Urls = [this.previewImage];
  }
}

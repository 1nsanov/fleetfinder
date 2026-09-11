import {AbstractControl} from "@angular/forms";

export interface SignInModel {
  Login: AbstractControl<string | null>
  Password: AbstractControl<string | null>
}

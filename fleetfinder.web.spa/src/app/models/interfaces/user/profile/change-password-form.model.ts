import {AbstractControl} from "@angular/forms";

export interface ChangePasswordForm {
  CurrentPassword: AbstractControl<string | null>;
  NewPassword: AbstractControl<string | null>;
}

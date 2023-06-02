import {AbstractControl, FormGroup} from "@angular/forms";

export interface ProfileForm {
  Email: AbstractControl<string | null>;
  Organization: AbstractControl<string | null>;
  FullName : FormGroup;
  Contact: FormGroup;
}

import {AbstractControl, FormGroup} from "@angular/forms";

export interface SignUpModel {
  Login: AbstractControl<string | null>;
  Password:  AbstractControl<string | null>;
  RepeatPassword:  AbstractControl<string | null>;
  Email:  AbstractControl<string | null>;
  FullName: FormGroup;
  Organization:  AbstractControl<string | null>;
}

 export interface FullName{
  First: AbstractControl<string | null>;
  Second:  AbstractControl<string | null>;
  Surname:  AbstractControl<string | null>;
}

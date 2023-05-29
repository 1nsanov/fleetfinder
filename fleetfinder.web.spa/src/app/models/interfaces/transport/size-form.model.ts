import {AbstractControl} from "@angular/forms";

export interface SizeForm{
  Length: AbstractControl<number | null>;
  Width: AbstractControl<number | null>;
  Height: AbstractControl<number | null>;
}

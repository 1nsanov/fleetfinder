import {AbstractControl} from "@angular/forms";

export interface PriceForm{
  PerHour: AbstractControl<string | null>;
  PerShift: AbstractControl<string | null>;
  PerKm: AbstractControl<string | null>;
}

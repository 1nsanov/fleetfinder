import {AbstractControl} from "@angular/forms";

export interface PriceForm{
  PerHour: AbstractControl<number | null>;
  PerShift: AbstractControl<number | null>;
  PerKm: AbstractControl<number | null>;
}

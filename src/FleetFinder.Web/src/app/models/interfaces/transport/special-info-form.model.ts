import {AbstractControl} from "@angular/forms";
import {SpecialType} from "../../enums/transport/special/special-type.enum";

export interface SpecialInfoForm {
  Type: AbstractControl<SpecialType | null>;
}

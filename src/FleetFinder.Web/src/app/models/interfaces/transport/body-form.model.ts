import {CargoBodyKind} from "../../enums/transport/cargo/cargo-body-kind.enum";
import {AbstractControl} from "@angular/forms";

export interface BodyForm{
  LoadCapacity: AbstractControl<number | null>;
  Length: AbstractControl<number | null>;
  Width: AbstractControl<number | null>;
  Height: AbstractControl<number | null>;
  Volume: AbstractControl<number | null>;
  Kind: AbstractControl<CargoBodyKind | null>;
}

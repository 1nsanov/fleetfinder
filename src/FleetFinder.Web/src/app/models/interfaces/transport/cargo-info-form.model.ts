import {AbstractControl, FormGroup} from "@angular/forms";
import {CargoType} from "../../enums/transport/cargo/cargo-type.enum";
import {CargoTransportationKind} from "../../enums/transport/cargo/cargo-transportation-kind";

export interface CargoInfoForm {
  Type: AbstractControl<CargoType | null>;
  Body: FormGroup;
  TransportationKind: AbstractControl<CargoTransportationKind | null>;
}

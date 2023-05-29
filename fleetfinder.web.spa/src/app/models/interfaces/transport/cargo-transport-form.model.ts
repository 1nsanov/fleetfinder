import {CargoType} from "../../enums/transport/cargo/cargo-type.enum";
import {CargoTransportationKind} from "../../enums/transport/cargo/cargo-transportation-kind";
import {Region} from "../../enums/common/region.enum";
import {AbstractControl, FormGroup} from "@angular/forms";
import { ExperienceWork } from "../../enums/transport/experience-work.enum";
import { PaymentMethod } from "../../enums/transport/payment-method.enum";
import { PaymentOrder } from "../../enums/transport/payment-order.enum";

export interface TransportForm {
  Id: AbstractControl<number | null>;
  Title: AbstractControl<string | null>;
  Region: AbstractControl<Region | null>;
  Brand: AbstractControl<string | null>;
  YearIssue: AbstractControl<string | null>;
  ExperienceWork: AbstractControl<ExperienceWork | null>;
  PaymentMethod: AbstractControl<PaymentMethod | null>;
  PaymentOrder: AbstractControl<PaymentOrder | null>;
  Price: FormGroup;
  Description: AbstractControl<string | null>;
  Images: AbstractControl<string[] | null>;

  // Unique: FormGroup;

  // Type: AbstractControl<CargoType | null>;
  // Body: FormGroup;
  // TransportationKind: AbstractControl<CargoTransportationKind | null>;
}

import {AbstractControl, FormGroup} from "@angular/forms";
import {PassengerType} from "../../enums/transport/passenger/passenger-type.enum";
import {PassengerRentalDuration} from "../../enums/transport/passenger/passenger-rental-duration.enum";
import {PassengerFacilities} from "../../enums/transport/passenger/passenger-facilities.enum";
import {PassengerOption} from "../../enums/transport/passenger/passenger-option.enum";
import {PassengerTransportationKind} from "../../enums/transport/passenger/passenger-transportation-kind.enum";

export interface PassengerInfoForm {
  Type: AbstractControl<PassengerType | null>;
  RentalDuration: AbstractControl<PassengerRentalDuration | null>;
  Facilities: AbstractControl<PassengerFacilities | null>;
  CountSeats: AbstractControl<any>;
  Size: FormGroup;
  Option: AbstractControl<PassengerOption | null>;
  TransportationKind: AbstractControl<PassengerTransportationKind | null>;
  Color: AbstractControl<string | null>;
  MinOrderTime: AbstractControl<number | null>;
}

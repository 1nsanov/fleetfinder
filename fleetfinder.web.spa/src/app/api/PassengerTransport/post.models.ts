import { Region } from "src/app/models/enums/common/region.enum";
import { ExperienceWork } from "src/app/models/enums/transport/experience-work.enum";
import { PaymentMethod } from "src/app/models/enums/transport/payment-method.enum";
import { PaymentOrder } from "src/app/models/enums/transport/payment-order.enum";
import {PriceModel} from "../Common/Transport/PriceModel";
import {SpecialType} from "../../models/enums/transport/special/special-type.enum";
import {PassengerType} from "../../models/enums/transport/passenger/passenger-type.enum";
import {PassengerRentalDuration} from "../../models/enums/transport/passenger/passenger-rental-duration.enum";
import {PassengerFacilities} from "../../models/enums/transport/passenger/passenger-facilities.enum";
import {SizeModel} from "../Common/Transport/SizeModel";
import {PassengerOption} from "../../models/enums/transport/passenger/passenger-option.enum";
import {PassengerTransportationKind} from "../../models/enums/transport/passenger/passenger-transportation-kind.enum";

export interface PassengerTransportPostRequestDto {
  Title: string;
  Region: Region;
  Brand: string | null;
  YearIssue: string | null;
  ExperienceWork: ExperienceWork | null;
  PaymentMethod: PaymentMethod | null;
  PaymentOrder: PaymentOrder | null;
  Price: PriceModel;
  Description: string | null;
  Type: PassengerType;
  RentalDuration: PassengerRentalDuration | null;
  Facilities: PassengerFacilities | null;
  CountSeats: number | null;
  Size: SizeModel;
  Option: PassengerOption | null;
  TransportationKind: PassengerTransportationKind | null;
  Color: string | null;
  MinOrderTime: number | null;
  Images: string[];
}

import {PassengerTransportPostRequestDto } from "./post.models";
import { ExperienceWork } from "src/app/models/enums/transport/experience-work.enum";
import { PaymentMethod } from "src/app/models/enums/transport/payment-method.enum";
import { PaymentOrder } from "src/app/models/enums/transport/payment-order.enum";
import {PriceModel} from "../Common/Transport/PriceModel";
import { Region } from "src/app/models/enums/common/region.enum";
import {PassengerFacilities} from "../../models/enums/transport/passenger/passenger-facilities.enum";
import {PassengerOption} from "../../models/enums/transport/passenger/passenger-option.enum";
import {PassengerRentalDuration} from "../../models/enums/transport/passenger/passenger-rental-duration.enum";
import {SizeModel} from "../Common/Transport/SizeModel";
import {PassengerTransportationKind} from "../../models/enums/transport/passenger/passenger-transportation-kind.enum";
import {PassengerType} from "../../models/enums/transport/passenger/passenger-type.enum";

export class PassengerTransportPutRequestDto implements PassengerTransportPostRequestDto {
  Id: number;
  Brand: string | null;
  Color: string | null;
  CountSeats: number | null;
  Description: string | null;
  ExperienceWork: ExperienceWork | null;
  Facilities: PassengerFacilities | null;
  Images: string[];
  MinOrderTime: number | null;
  Option: PassengerOption | null;
  PaymentMethod: PaymentMethod | null;
  PaymentOrder: PaymentOrder | null;
  Price: PriceModel;
  Region: Region;
  RentalDuration: PassengerRentalDuration | null;
  Size: SizeModel;
  Title: string;
  TransportationKind: PassengerTransportationKind | null;
  Type: PassengerType;
  YearIssue: string | null;
}

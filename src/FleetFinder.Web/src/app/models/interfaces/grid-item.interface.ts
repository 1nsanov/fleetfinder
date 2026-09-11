import {CargoTransportationKind} from "../enums/transport/cargo/cargo-transportation-kind";
import { Region } from "../enums/common/region.enum";
import { Contact } from "src/app/api/Common/Contact";
import {PriceModel} from "../../api/Common/Transport/PriceModel";
import {PassengerFacilities} from "../enums/transport/passenger/passenger-facilities.enum";
import {PassengerTransportationKind} from "../enums/transport/passenger/passenger-transportation-kind.enum";

export interface IGridItem {
  Id: number;
  Title: string;
  Region: Region;
  Type: any;
  Price: PriceModel;
  Contact: Contact;
  Images: string[];

  //nullable
  TransportationKind: CargoTransportationKind | PassengerTransportationKind | null;
  Facilities: PassengerFacilities | null;
  Description: string | null;
}

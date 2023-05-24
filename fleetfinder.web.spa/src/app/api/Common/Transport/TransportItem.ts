import {PriceModel} from "./PriceModel";
import {CargoTransportationKind} from "../../../models/enums/transport/cargo/cargo-transportation-kind";
import { Region } from "src/app/models/enums/common/region.enum";
import { ExperienceWork } from "src/app/models/enums/transport/experience-work.enum";
import { Contact } from "../Contact";
import {PassengerFacilities} from "../../../models/enums/transport/passenger/passenger-facilities.enum";

export interface TransportItem<T> {
  Id: number;
  Title: string;
  Region: Region;
  Brand: string | null;
  YearIssue: string | null;
  ExperienceWork: ExperienceWork | null;
  Price: PriceModel;
  Description: string | null;
  Type: T;
  TransportationKind: CargoTransportationKind | null;
  Facilities: PassengerFacilities | null;
  // TransportationKind: PassengerTransportationKind | null;
  Contact: Contact;
  Images: string[];
}

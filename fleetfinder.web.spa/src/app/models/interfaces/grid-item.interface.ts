import {CargoType} from "../enums/transport/cargo/cargo-type.enum";
import {CargoTransportationKind} from "../enums/transport/cargo/cargo-transportation-kind";
import { Region } from "../enums/common/region.enum";
import { ExperienceWork } from "../enums/transport/experience-work.enum";
import { Price } from "src/app/api/Common/Transport/Price";
import { Contact } from "src/app/api/Common/Contact";

export interface IGridItem {
  Id: number;
  Title: string;
  Region: Region;
  Type: CargoType;
  Price: Price;
  Contact: Contact;
  Images: string[];

  //nullable
  Brand: string | null;
  YearIssue: string | null;
  ExperienceWork: ExperienceWork | null;
  Description: string | null;
  TransportationKind: CargoTransportationKind | null;
}

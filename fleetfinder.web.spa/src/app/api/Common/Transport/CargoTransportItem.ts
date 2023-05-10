import { Region } from "src/app/models/enums/common/region.enum";
import { ExperienceWork } from "src/app/models/enums/transport/experience-work.enum";
import {CargoType} from "../../../models/enums/transport/cargo/cargo-type.enum";
import { Contact } from "../Contact";
import { Price } from "./Price";

export class CargoTransportItem {
  Id: number;
  Title: string;
  Region: Region;
  Brand?: string | null;
  ExperienceWork?: ExperienceWork | null;
  Price: Price;
  Description?: string | null;
  Type: CargoType;
  Images: string[];
  Contact: Contact;
}

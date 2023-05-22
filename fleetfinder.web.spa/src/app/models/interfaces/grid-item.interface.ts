import {CargoTransportationKind} from "../enums/transport/cargo/cargo-transportation-kind";
import { Region } from "../enums/common/region.enum";
import { Contact } from "src/app/api/Common/Contact";
import {PriceModel} from "../../api/Common/Transport/PriceModel";

export interface IGridItem {
  Id: number;
  Title: string;
  Region: Region;
  Type: any;
  Price: PriceModel;
  Contact: Contact;
  Images: string[];

  //nullable
  TransportationKind: CargoTransportationKind | null;
  Description: string | null;
}

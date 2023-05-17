import {CargoTransportPostRequestDto} from "./post.models";
import {BodyModel} from "../Common/Transport/BodyModel";
import { ExperienceWork } from "src/app/models/enums/transport/experience-work.enum";
import { PaymentMethod } from "src/app/models/enums/transport/payment-method.enum";
import { PaymentOrder } from "src/app/models/enums/transport/payment-order.enum";
import {PriceModel} from "../Common/Transport/PriceModel";
import { Region } from "src/app/models/enums/common/region.enum";
import {CargoTransportationKind} from "../../models/enums/transport/cargo/cargo-transportation-kind";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";

export class CargoTransportPutRequestDto implements CargoTransportPostRequestDto {
  Id: number;
  Body: BodyModel;
  Brand: string | null;
  Description: string | null;
  ExperienceWork: ExperienceWork | null;
  Images: string[];
  PaymentMethod: PaymentMethod | null;
  PaymentOrder: PaymentOrder | null;
  Price: PriceModel;
  Region: Region;
  Title: string;
  TransportationKind: CargoTransportationKind | null;
  Type: CargoType;
  YearIssue: string | null;

}

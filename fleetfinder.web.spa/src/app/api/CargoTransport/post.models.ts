import { Region } from "src/app/models/enums/common/region.enum";
import { ExperienceWork } from "src/app/models/enums/transport/experience-work.enum";
import { PaymentMethod } from "src/app/models/enums/transport/payment-method.enum";
import { PaymentOrder } from "src/app/models/enums/transport/payment-order.enum";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";
import {BodyModel} from "../Common/Transport/BodyModel";
import {CargoTransportationKind} from "../../models/enums/transport/cargo/cargo-transportation-kind";
import {PriceModel} from "../Common/Transport/PriceModel";

export interface CargoTransportPostRequestDto {
  Title: string;
  Region: Region;
  Brand: string | null;
  YearIssue: string | null;
  ExperienceWork: ExperienceWork | null;
  PaymentMethod: PaymentMethod | null;
  PaymentOrder: PaymentOrder | null;
  Price: PriceModel;
  Description: string | null;
  Type: CargoType;
  Body: BodyModel;
  TransportationKind: CargoTransportationKind | null;
  Images: string[];
}

import { Region } from "src/app/models/enums/common/region.enum";
import { ExperienceWork } from "src/app/models/enums/transport/experience-work.enum";
import { PaymentMethod } from "src/app/models/enums/transport/payment-method.enum";
import { PaymentOrder } from "src/app/models/enums/transport/payment-order.enum";
import { Price } from "../Common/Transport/Price";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";
import {BodyModel} from "../Common/Transport/BodyModel";
import {CargoTransportationKind} from "../../models/enums/transport/cargo/cargo-transportation-kind";

export class CargoTransportPostRequestDto {
  Title: string;
  Region: Region | null = null;
  Brand: string;
  YearIssue: string;
  ExperienceWork: ExperienceWork | null = null;
  PaymentMethod: PaymentMethod | null = null;
  PaymentOrder: PaymentOrder | null = null;
  Price: Price = new Price();
  Description: string;
  Type: CargoType | null = null;
  Body: BodyModel = new BodyModel();
  TransportationKind: CargoTransportationKind | null = null;
  Images: string[] = [];
}

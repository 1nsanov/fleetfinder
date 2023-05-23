import { Region } from "src/app/models/enums/common/region.enum";
import { ExperienceWork } from "src/app/models/enums/transport/experience-work.enum";
import { PaymentMethod } from "src/app/models/enums/transport/payment-method.enum";
import { PaymentOrder } from "src/app/models/enums/transport/payment-order.enum";
import {Contact} from "../Common/Contact";
import {PriceModel} from "../Common/Transport/PriceModel";
import {SpecialType} from "../../models/enums/transport/special/special-type.enum";

export interface SpecialTransportGetResponse {
  Id: number;
  Title: string;
  Region: Region;
  Brand: string | null;
  YearIssue: string | null;
  ExperienceWork: ExperienceWork | null;
  PaymentMethod: PaymentMethod | null;
  PaymentOrder: PaymentOrder | null;
  Price: PriceModel;
  Description: string | null;
  Type: SpecialType;
  Images: string[];
  Contact: Contact;
  CreateDate: Date;
  UserId: number;
}

import {CargoTransportSortParameter} from "../../models/enums/transport/cargo/cargo-transport-sort-parameter.enum";
import {TransportFilter} from "../../models/transport/transport-filter.model";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";
import {Region} from "../../models/enums/common/region.enum";
import {ExperienceWork} from "../../models/enums/transport/experience-work.enum";

//---GetList---
export class CargoTransportGetListRequestDto {
    pageSize : number = 6;
    skipCount : number = 0;
    sortParameter : CargoTransportSortParameter = CargoTransportSortParameter.Default;
    sortDesc: boolean = false;
    filter : TransportFilter<CargoType> = new TransportFilter<CargoType>();
}

export class CargoTransportGetListResponseDto {
    Items : CargoTransportDto[] = [];
    TotalCount : number = 0;
}

export class CargoTransportDto {
  Id: number;
  Title: string;
  Region: Region;
  Brand?: string | null;
  ExperienceWork?: ExperienceWork | null;
  Price: PriceDto;
  Description?: string | null;
  Type: CargoType;
  Images: string[];
  Contact: ContactDto;
}

class PriceDto {
  PerHour?: number | null;
  PerShift?: number | null;
  PerKm?: number | null;
}

class ContactDto {
  Title: string;
  PhoneViber?: string | null;
  PhoneTelegram?: string | null;
  PhoneWhatsapp?: string | null;
  WorkingMode?: string | null;
}



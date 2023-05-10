import {CargoTransportSortParameter} from "../../models/enums/transport/cargo/cargo-transport-sort-parameter.enum";
import {TransportFilter} from "../../models/transport/transport-filter.model";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";
import {CargoTransportItem} from "../Common/Transport/CargoTransportItem";

export class CargoTransportGetListRequestDto {
    pageSize : number = 6;
    skipCount : number = 0;
    sortParameter : CargoTransportSortParameter = CargoTransportSortParameter.Default;
    sortDesc: boolean = false;
    filter : TransportFilter<CargoType> = new TransportFilter<CargoType>();
}
export class CargoTransportGetListResponseDto {
    Items : CargoTransportItem[] = [];
    TotalCount : number = 0;
}






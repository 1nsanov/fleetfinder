import {TransportSortParameter} from "../../models/enums/transport/cargo/cargo-transport-sort-parameter.enum";
import {TransportFilter} from "../../models/transport/transport-filter.model";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";
import {TransportItem} from "../Common/Transport/TransportItem";

export class CargoTransportGetListRequestDto {
    pageSize : number = 6;
    skipCount : number = 0;
    sortParameter : TransportSortParameter = TransportSortParameter.Default;
    sortDesc: boolean = false;
    filter : TransportFilter<CargoType> = new TransportFilter<CargoType>();
}
export interface CargoTransportGetListResponseDto {
    Items : TransportItem<CargoType>[];
    TotalCount : number;
}






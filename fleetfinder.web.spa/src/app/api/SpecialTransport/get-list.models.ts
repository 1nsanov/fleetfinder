import {TransportSortParameter} from "../../models/enums/transport/cargo/cargo-transport-sort-parameter.enum";
import {TransportFilter} from "../../models/transport/transport-filter.model";
import {SpecialType} from "../../models/enums/transport/special/special-type.enum";
import {TransportItem} from "../Common/Transport/TransportItem";

export class SpecialTransportGetListRequestDto {
    pageSize : number = 6;
    skipCount : number = 0;
    sortParameter : TransportSortParameter = TransportSortParameter.Default;
    sortDesc: boolean = false;
    filter : TransportFilter<SpecialType> = new TransportFilter<SpecialType>();
}
export interface CargoTransportGetListResponseDto {
    Items : TransportItem<SpecialType>[];
    TotalCount : number;
}






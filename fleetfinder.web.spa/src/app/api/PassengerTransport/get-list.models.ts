import {TransportSortParameter} from "../../models/enums/transport/cargo/cargo-transport-sort-parameter.enum";
import {TransportFilter} from "../../models/transport/transport-filter.model";
import {SpecialType} from "../../models/enums/transport/special/special-type.enum";
import {TransportItem} from "../Common/Transport/TransportItem";
import {PassengerType} from "../../models/enums/transport/passenger/passenger-type.enum";

export class PassengerTransportGetListRequestDto {
    pageSize : number = 6;
    skipCount : number = 0;
    sortParameter : TransportSortParameter = TransportSortParameter.Default;
    sortDesc: boolean = false;
    filter : TransportFilter<SpecialType> = new TransportFilter<SpecialType>();
}
export interface PassengerTransportGetListResponseDto {
    Items : TransportItem<PassengerType>[];
    TotalCount : number;
}






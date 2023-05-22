import {DropdownItemModel} from "../models/dropdown-item.model";
import {Region} from "../models/enums/common/region.enum";
import {
  CargoBodyKindConst,
  CargoTransportationKindConst,
  CargoTypeConst,
  ExperienceWorkConst,
  PaymentMethodConst,
  PaymentOrderConst,
  RegionConst, SpecialTypeConst,
  TransportTypeConst
} from "./enums.data";
import {ExperienceWork} from "../models/enums/transport/experience-work.enum";
import {PaymentMethod} from "../models/enums/transport/payment-method.enum";
import {PaymentOrder} from "../models/enums/transport/payment-order.enum";
import {TransportType} from "../models/enums/transport/transport-type.enum";
import {CargoBodyKind} from "../models/enums/transport/cargo/cargo-body-kind.enum";
import {CargoTransportationKind} from "../models/enums/transport/cargo/cargo-transportation-kind";
import {CargoType} from "../models/enums/transport/cargo/cargo-type.enum";
import {SpecialType} from "../models/enums/transport/special/special-type.enum";
import {SortModel} from "../models/sort.model";
import {TransportSortParameter} from "../models/enums/transport/cargo/cargo-transport-sort-parameter.enum";

export function getRegionItems() {
  return [
    new DropdownItemModel<Region | null>(null, "Не выбрано"),
    ...Object.values(Region).map(x =>
      new DropdownItemModel<Region>(x as Region, RegionConst[x as Region])
    )
  ];
}

export function getExperienceWorkItems() {
  return [
    new DropdownItemModel<ExperienceWork | null>(null, "Не выбрано"),
    ...Object.values(ExperienceWork).map(x =>
      new DropdownItemModel<ExperienceWork>(x as ExperienceWork, ExperienceWorkConst[x as ExperienceWork])
    )
  ];
}

export function getPaymentMethodItems() {
  return [
    new DropdownItemModel<PaymentMethod | null>(null, "Не выбрано"),
    ...Object.values(PaymentMethod).map(x =>
      new DropdownItemModel<PaymentMethod>(x as PaymentMethod, PaymentMethodConst[x as PaymentMethod])
    )
  ];
}

export function getPaymentOrderItems() {
  return [
    new DropdownItemModel<PaymentOrder | null>(null, "Не выбрано"),
    ...Object.values(PaymentOrder).map(x =>
      new DropdownItemModel<PaymentOrder>(x as PaymentOrder, PaymentOrderConst[x as PaymentOrder])
    )
  ];
}

// export function getTransportTypeItems() {
//   return Object.values(TransportType).map(x =>
//     new DropdownItemModel<TransportType>(x as TransportType, TransportTypeConst[x as TransportType])
//   );
// }

export function getCargoBodyKindItems() {
  return [
    new DropdownItemModel<CargoBodyKind | null>(null, "Не выбрано"),
    ...Object.values(CargoBodyKind).map(x =>
      new DropdownItemModel<CargoBodyKind>(x as CargoBodyKind, CargoBodyKindConst[x as CargoBodyKind])
    )
  ];
}

export function getCargoTransportationKindItems() {
  return [
    new DropdownItemModel<CargoTransportationKind | null>(null, "Не выбрано"),
    ...Object.values(CargoTransportationKind).map(x =>
      new DropdownItemModel<CargoTransportationKind>(x as CargoTransportationKind, CargoTransportationKindConst[x as CargoTransportationKind])
    )
  ];
}

export function getCargoTypeItems() {
  return [
    new DropdownItemModel<CargoType | null>(null, "Не выбрано"),
    ...Object.values(CargoType).map(x =>
      new DropdownItemModel<CargoType>(x as CargoType, CargoTypeConst[x as CargoType])
    )
  ];
}

export function getSpecialItems() {
  return [
    new DropdownItemModel<SpecialType | null>(null, "Не выбрано"),
    ...Object.values(SpecialType).map(x =>
      new DropdownItemModel<SpecialType>(x as SpecialType, SpecialTypeConst[x as SpecialType])
    )
  ];
}

export function getYearItems(){
  const years: DropdownItemModel<string>[] = [new DropdownItemModel<string>('', 'Не выбрано')]
  for (let i = 2023; i >= 1960; i--) {
    years.push(new DropdownItemModel<string>(i.toString(), i.toString()))
  }
  return years;
}

export function getSortParameters(){
  return [
    { Value: new SortModel(TransportSortParameter.Default, false), Preview: "По дате создания (в)" },
    { Value: new SortModel(TransportSortParameter.Default, true), Preview: "По дате создания (у)" },
    { Value: new SortModel(TransportSortParameter.PricePerHour, false), Preview: "По цене за час (в)" },
    { Value: new SortModel(TransportSortParameter.PricePerHour, true), Preview: "По цене за час (у)" },
    { Value: new SortModel(TransportSortParameter.PricePerShift, false), Preview: "По цене за смену (в)" },
    { Value: new SortModel(TransportSortParameter.PricePerShift, true), Preview: "По цене за смену (у)" },
    { Value: new SortModel(TransportSortParameter.PricePerKm, false) , Preview: "По цене  за километр (в)" },
    { Value: new SortModel(TransportSortParameter.PricePerKm, true) , Preview: "По цене  за километр (у)" },
  ]
}

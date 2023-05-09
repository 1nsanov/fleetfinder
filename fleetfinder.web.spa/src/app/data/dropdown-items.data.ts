﻿import {DropdownItemModel} from "../models/dropdown-item.model";
import {Region} from "../models/enums/common/region.enum";
import {
  CargoBodyKindConst, CargoTransportationKindConst, CargoTypeConst,
  ExperienceWorkConst,
  PaymentMethodConst,
  PaymentOrderConst,
  RegionConst,
  TransportTypeConst
} from "./enums.data";
import {ExperienceWork} from "../models/enums/transport/experience-work.enum";
import {PaymentMethod} from "../models/enums/transport/payment-method.enum";
import {PaymentOrder} from "../models/enums/transport/payment-order.enum";
import {TransportType} from "../models/enums/transport/transport-type.enum";
import {CargoBodyKind} from "../models/enums/transport/cargo/cargo-body-kind.enum";
import {CargoTransportationKind} from "../models/enums/transport/cargo/cargo-transportation-kind";
import {CargoType} from "../models/enums/transport/cargo/cargo-type.enum";

export function getRegionItems() {
  return Object.values(Region).map(x =>
    new DropdownItemModel<Region>(x as Region, RegionConst[x as Region])
  );
}

export function getExperienceWorkItems() {
  return Object.values(ExperienceWork).map(x =>
    new DropdownItemModel<ExperienceWork>(x as ExperienceWork, ExperienceWorkConst[x as ExperienceWork])
  );
}

export function getPaymentMethodItems() {
  return Object.values(PaymentMethod).map(x =>
    new DropdownItemModel<PaymentMethod>(x as PaymentMethod, PaymentMethodConst[x as PaymentMethod])
  );
}

export function getPaymentOrderItems() {
  return Object.values(PaymentOrder).map(x =>
    new DropdownItemModel<PaymentOrder>(x as PaymentOrder, PaymentOrderConst[x as PaymentOrder])
  );
}

export function getTransportTypeItems() {
  return Object.values(TransportType).map(x =>
    new DropdownItemModel<TransportType>(x as TransportType, TransportTypeConst[x as TransportType])
  );
}

export function getCargoBodyKindItems() {
  return Object.values(CargoBodyKind).map(x =>
    new DropdownItemModel<CargoBodyKind>(x as CargoBodyKind, CargoBodyKindConst[x as CargoBodyKind])
  );
}

export function getCargoTransportationKindItems() {
  return Object.values(CargoTransportationKind).map(x =>
    new DropdownItemModel<CargoTransportationKind>(x as CargoTransportationKind, CargoTransportationKindConst[x as CargoTransportationKind])
  );
}

export function getCargoTypeItems() {
  return Object.values(CargoType).map(x =>
    new DropdownItemModel<CargoType>(x as CargoType, CargoTypeConst[x as CargoType])
  );
}
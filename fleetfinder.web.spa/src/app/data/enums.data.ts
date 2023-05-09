import {Region} from "../models/enums/common/region.enum";
import {ExperienceWork} from "../models/enums/transport/experience-work.enum";
import {PaymentMethod} from "../models/enums/transport/payment-method.enum";
import {PaymentOrder} from "../models/enums/transport/payment-order.enum";
import {TransportType} from "../models/enums/transport/transport-type.enum";
import {CargoBodyKind} from "../models/enums/transport/cargo/cargo-body-kind.enum";
import {CargoTransportationKind} from "../models/enums/transport/cargo/cargo-transportation-kind";
import {CargoType} from "../models/enums/transport/cargo/cargo-type.enum";

export const RegionConst: Record<Region, string> = {
  [Region.Bender]: 'Бендеры',
  [Region.Tiraspol]: 'Тирасполь',
  [Region.Grigoriopol]: 'Григориопль',
  [Region.Dubasari]: 'Дубоссары',
  [Region.Camenka]: 'Каменка',
  [Region.Ribnitsa]: 'Рыбница',
  [Region.Slobozia]: 'Слободзея',
}

export const ExperienceWorkConst: Record<ExperienceWork, string> = {
  [ExperienceWork.LessYear1]: 'Менее 1 года',
  [ExperienceWork.Year1]: '1 год',
  [ExperienceWork.Year2]: '2 года',
  [ExperienceWork.Year3]: '3 года',
  [ExperienceWork.Year4]: '4 года',
  [ExperienceWork.Year5]: '5 лет',
  [ExperienceWork.Year6]: '6 лет',
  [ExperienceWork.Year7]: '7 лет',
  [ExperienceWork.Year8]: '8 лет',
  [ExperienceWork.Year9]: '9 лет',
  [ExperienceWork.MoreYear10]: 'Более 10 лет'
};

export const PaymentMethodConst: Record<PaymentMethod, string> = {
  [PaymentMethod.Cash]: 'Наличный расчет',
  [PaymentMethod.NonCash]: 'Безналичный расчет',
  [PaymentMethod.CashAndNonCash]: 'Наличный и безналичный расчет',
  [PaymentMethod.CardPayment]: 'Оплата картой'
};

export const PaymentOrderConst: Record<PaymentOrder, string> = {
  [PaymentOrder.Prepayment]: 'Предоплата',
  [PaymentOrder.PaymentUponDelivery]: 'Оплата по факту',
  [PaymentOrder.InstallmentPayment]: 'Поэтапная оплата',
};

export const TransportTypeConst: Record<TransportType, string> = {
  [TransportType.Cargo]: 'Грузовой',
  [TransportType.Passenger]: 'Пассажирский',
  [TransportType.Special]: 'Спецтехника',
};

export const CargoBodyKindConst: Record<CargoBodyKind, string> = {
  [CargoBodyKind.AutoTrain]: 'Автопоезд',
  [CargoBodyKind.BeamVehicle]: 'Балковоз',
  [CargoBodyKind.FlatbedVehicle]: 'Бортовой',
  [CargoBodyKind.HydraulicLift]: 'Гидроборт',
  [CargoBodyKind.CargoPassenger]: 'Грузопассажирский',
  [CargoBodyKind.OversizedVehicle]: 'Длинномер',
  [CargoBodyKind.IsothermalVehicle]: 'Изотермический',
  [CargoBodyKind.ContainerVehicle]: 'Контейнеровоз',
  [CargoBodyKind.FeedVehicle]: 'Кормовоз',
  [CargoBodyKind.FlourVehicle]: 'Муковоз',
  [CargoBodyKind.OpenVehicle]: 'Открытый',
  [CargoBodyKind.PanelVehicle]: 'Панелевоз',
  [CargoBodyKind.Pickup]: 'Пикап',
  [CargoBodyKind.Pyramid]: 'Пирамида',
  [CargoBodyKind.SemiTrailer]: 'Полуприцеп',
  [CargoBodyKind.LightTrailer]: 'Прицеп легковой',
  [CargoBodyKind.Refrigerator]: 'Рефрижератор',
  [CargoBodyKind.RollVehicle]: 'Рулоновоз',
  [CargoBodyKind.TractorUnit]: 'Седельный тягач',
  [CargoBodyKind.AgriculturalGrainVehicle]: 'Сельхозник/зерновоз',
  [CargoBodyKind.LivestockVehicle]: 'Скотовоз',
  [CargoBodyKind.TimberVehicle]: 'Сортиментовоз/лесовоз',
  [CargoBodyKind.GlassVehicle]: 'Стекловоз',
  [CargoBodyKind.Coupling]: 'Сцепка',
  [CargoBodyKind.TankContainer]: 'Танк-контейнер',
  [CargoBodyKind.CurtainSider]: 'Тентованный',
  [CargoBodyKind.InsulatedVehicle]: 'Термобудка',
  [CargoBodyKind.PipeVehicle]: 'Трубовоз',
  [CargoBodyKind.TrailerTruck]: 'Фура',
  [CargoBodyKind.Van]: 'Фургон',
  [CargoBodyKind.SolidMetal]: 'Цельнометаллический',
  [CargoBodyKind.ChipCar]: 'Щеповоз'
};

export const CargoTransportationKindConst: Record<CargoTransportationKind, string> = {
  [CargoTransportationKind.CargoTaxi]: 'Грузовое такси',
  [CargoTransportationKind.ApartmentMoving]: 'Квартирный переезд',
  [CargoTransportationKind.OfficeMoving]: 'Офисный переезд',
  [CargoTransportationKind.FurnitureTransport]: 'Перевозка мебели',
  [CargoTransportationKind.FoodTransport]: 'Перевозка продуктов',
  [CargoTransportationKind.IntercityTransport]: 'Междугородние перевозки',
  [CargoTransportationKind.InternationalTransport]: 'Международные перевозки',
  [CargoTransportationKind.LCLTransport]: 'Доставка сборных грузов',
  [CargoTransportationKind.LivestockTransport]: 'Перевозка лошадей и крупного рогатого скота',
  [CargoTransportationKind.PersonalItemsTransport]: 'Перевозка личных вещей',
  [CargoTransportationKind.ApplianceTransport]: 'Перевозка бытовой техники',
  [CargoTransportationKind.FruitTransport]: 'Перевозка фруктов',
  [CargoTransportationKind.VegetableTransport]: 'Перевозка овощей',
  [CargoTransportationKind.CountryHouseMoving]: 'Дачный переезд',
  [CargoTransportationKind.WarehouseMoving]: 'Переезд склада',
  [CargoTransportationKind.PianoTransport]: 'Перевозка пианино',
  [CargoTransportationKind.SafeTransport]: 'Перевозка сейфов',
  [CargoTransportationKind.RefrigeratorTransport]: 'Перевозка холодильников',
  [CargoTransportationKind.EquipmentTransport]: 'Перевозка оборудования',
  [CargoTransportationKind.ConstructionMaterialsTransport]: 'Перевозка строительных материалов',
  [CargoTransportationKind.MotorcycleTransport]: 'Перевозка мотоциклов',
};

export const CargoTypeConst: Record<CargoType, string> = {
  [CargoType.T1]: 'До 1 т.',
  [CargoType.T2]: 'До 2 т.',
  [CargoType.T3]: 'До 3,5 т.',
  [CargoType.T5]: 'До 5 т.',
  [CargoType.T10]: 'До 10 т.',
  [CargoType.T20]: 'До 20 т.',
};

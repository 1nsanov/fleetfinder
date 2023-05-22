import {IInfoBoxTransport} from "../../models/interfaces/info-box-transport.interface";
import {SpecialType} from "../../models/enums/transport/special/special-type.enum";

export const specialItems : Array<IInfoBoxTransport> = [
  { Icon: "excavator", Text: "Экскаваторы", Value: SpecialType.Excavator },
  { Icon: "aerial-platform", Text: "Автовышки", Value: SpecialType.AerialPlatform },
  { Icon: "truck-crane", Text: "Автокраны", Value: SpecialType.TruckCrane },
  { Icon: "truck-fuel", Text: "Бензовозы", Value: SpecialType.TruckFuel },
  { Icon: "bulldozer", Text: "Бульдозеры", Value: SpecialType.Bulldozer },
  { Icon: "hydra-hammer", Text: "Гидромолоты", Value: SpecialType.HydraHammer },
  { Icon: "graders", Text: "Грейдеры", Value: SpecialType.Grader },
  { Icon: "crag-loader", Text: "Грефейные погрузчики", Value: SpecialType.CragLoader },
  { Icon: "road-rollers", Text: "Дорожные катки", Value: SpecialType.RoadRoller },
  { Icon: "mini-loader", Text: "Мини-погрузчики", Value: SpecialType.MiniLoader },
  { Icon: "mini-excavator", Text: "Мини-экскаваторы", Value: SpecialType.MiniExcavator },
  { Icon: "wastecar", Text: "Мусоровозы", Value: SpecialType.WasteCar },
  { Icon: "dump-truck", Text: "Самосвалы", Value: SpecialType.DumpTruck },
  { Icon: "tractor", Text: "Тракторы", Value: SpecialType.Tractor },
  { Icon: "front-loader", Text: "Фронтальные\n" + "погрузчики", Value: SpecialType.FrontLoader },
  { Icon: "truck-cement", Text: "Бетоновозы", Value: SpecialType.TruckCement },
  { Icon: "excavator-loader", Text: "Эскаваторы-погрузчики", Value: SpecialType.ExcavatorLoader },
  { Icon: "yamobur", Text: "Ямобуры и сваебои", Value: SpecialType.Yamobur },
]

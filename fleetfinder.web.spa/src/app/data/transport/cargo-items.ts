import {IInfoBoxTransport} from "../../models/interfaces/info-box-transport.interface";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";

export const cargoItems : Array<IInfoBoxTransport> = [
  { Icon: "1t", Text: "До 1 т.", Value: CargoType.T1 },
  { Icon: "2t", Text: "До 2 т." , Value: CargoType.T2},
  { Icon: "3.5t", Text: "До 3,5 т." , Value: CargoType.T3},
  { Icon: "5t", Text: "До 5 т.", Value: CargoType.T5 },
  { Icon: "10t", Text: "До 10 т.", Value: CargoType.T10 },
  { Icon: "20t", Text: "До 20 т.", Value: CargoType.T20 },
]

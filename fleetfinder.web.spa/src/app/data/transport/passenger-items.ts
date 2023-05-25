import {IInfoBoxTransport} from "../../models/interfaces/info-box-transport.interface";
import {PassengerType} from "../../models/enums/transport/passenger/passenger-type.enum";

export const passengerItems : Array<IInfoBoxTransport> = [
  { Icon: "taxi", Text: "Такси", Value: PassengerType.Taxi},
  { Icon: "limousine", Text: "Лимузины", Value: PassengerType.Limousine },
  { Icon: "minivan", Text: "Минивэны", Value: PassengerType.Minivan },
  { Icon: "bus", Text: "Автобусы", Value: PassengerType.Bus },
  { Icon: "shiftw", Text: "Вахтовики", Value: PassengerType.Shiftw },
  { Icon: "water", Text: "Водный", Value: PassengerType.Water },
]

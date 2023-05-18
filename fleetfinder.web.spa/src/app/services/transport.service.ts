import { Injectable } from '@angular/core';
import {IInfoBoxTransport} from "../models/interfaces/info-box-transport.interface";
import {CargoType} from "../models/enums/transport/cargo/cargo-type.enum";
import {TransportType} from "../models/enums/transport/transport-type.enum";
import {CargoTransportGetListRequestDto} from "../api/CargoTransport/get-list.models";

@Injectable({
  providedIn: 'root'
})
export class TransportService {
  constructor() { }

  public saveListRequest(key: string, request: CargoTransportGetListRequestDto) : void {
    const requestJson = JSON.stringify(request);
    localStorage.setItem(key, requestJson);
  }
  public getListRequest(key: string) : CargoTransportGetListRequestDto | null{
    const requestJson = localStorage.getItem(key);
    return requestJson ? JSON.parse(requestJson) : null;
  }

  public clearListRequest(key: string) : void {
    localStorage.removeItem(key);
  }

  public getTypeImg(item: IInfoBoxTransport, type: TransportType) : string {
    switch (type){
      case TransportType.Cargo:
        return  '../../../assets/icons/transport/cargo/icon-cargo-' + item.Icon + '.svg';
      case TransportType.Passenger:
        return  '../../../assets/icons/transport/passenger/icon-passenger-' + item.Icon + '.svg';
      case TransportType.Special:
        return '../../../assets/icons/transport/special/icon-' + item.Icon + '.png';
    }
  }
}

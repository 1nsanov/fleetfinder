import {Injectable} from '@angular/core';
import {IInfoBoxTransport} from "../models/interfaces/info-box-transport.interface";
import {TransportType} from "../models/enums/transport/transport-type.enum";
import {CargoTransportGetListRequestDto} from "../api/CargoTransport/get-list.models";
import {SpecialTransportGetListRequestDto} from "../api/SpecialTransport/get-list.models";
import {PassengerTransportGetListRequestDto} from "../api/PassengerTransport/get-list.models";
import {PassengerType} from "../models/enums/transport/passenger/passenger-type.enum";

@Injectable({
  providedIn: 'root'
})
export class TransportService {
  constructor() { }

  public saveListRequest(key: string, request: any) : void {
    const requestJson = JSON.stringify(request);
    localStorage.setItem(key, requestJson);
  }

  public getCargoListRequest(key: string) : CargoTransportGetListRequestDto | null {
    return this.getListRequest(key);
  }

  public getPassengerListRequest(key: string) : PassengerTransportGetListRequestDto | null {
    return this.getListRequest(key);
  }

  public getSpecialListRequest(key: string) : SpecialTransportGetListRequestDto | null {
    return this.getListRequest(key);
  }

  private getListRequest(key: string) : any | null{
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

  //#region passenger showing field

  public isShowRentalDuration(type: PassengerType) {
    return this.isBus(type) || this.isMinivan(type)
  }

  public isShowFacilities(type: PassengerType) {
    return this.isBus(type) || this.isMinivan(type) || this.isTaxi(type);
  }

  public isShowCountSeats(type: PassengerType) {
    return !this.isTaxi(type);
  }

  public isShowSize(type: PassengerType) {
    return this.isBus(type);
  }

  public isShowOption(type: PassengerType) {
    return this.isBus(type) || this.isMinivan(type)
  }

  public isShowColor(type: PassengerType) {
    return this.isBus(type) || this.isMinivan(type)
  }


  private isTaxi(type: PassengerType) : boolean {
    return type === PassengerType.Taxi;
  }

  private isBus(type: PassengerType) : boolean {
    return type === PassengerType.Bus;
  }

  private isMinivan(type: PassengerType) : boolean {
    return type === PassengerType.Minivan;
  }

  //#endregion
}

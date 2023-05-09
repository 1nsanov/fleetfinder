import { Component } from '@angular/core';
import {
  getCargoBodyKindItems, getCargoTransportationKindItems, getCargoTypeItems,
  getExperienceWorkItems,
  getPaymentMethodItems,
  getPaymentOrderItems,
  getRegionItems, getTransportTypeItems
} from "../../data/dropdown-items.data";
import {ModalService} from "../../services/modal.service";
import {CargoTypeConst, TransportTypeConst} from "../../data/enums.data";
import {DropdownItemModel} from "../../models/dropdown-item.model";
import {TransportType} from "../../models/enums/transport/transport-type.enum";
import {cargoItems} from "../../data/transport/cargo-items";
import {passengerItems} from "../../data/transport/passenger-items";
import {specialItems} from "../../data/transport/special-items";
import {IInfoBoxTransport} from "../../models/interfaces/info-box-transport.interface";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";

@Component({
  selector: 'app-add-transport-page',
  templateUrl: './add-transport-page.component.html',
  styleUrls: ['./add-transport-page.component.scss']
})
export class AddTransportPageComponent {
  constructor(public modalService: ModalService) {
  }
  RegionItems = getRegionItems();
  ExperienceWorkItems = getExperienceWorkItems();
  PaymentMethodItems = getPaymentMethodItems();
  PaymentOrderItems = getPaymentOrderItems();
  CargoBodyKindItems = getCargoBodyKindItems();
  CargoTransportationKindItems = getCargoTransportationKindItems();
  cargo = cargoItems;
  passenger = passengerItems;
  special = specialItems;
  TransportType = TransportType;
  currentType: TransportType | null = null;


  onSelectTransportType(item: IInfoBoxTransport, type: TransportType){
    this.currentType = type;
    switch (type){
        case TransportType.Cargo:
            console.log(item.Value as CargoType)
            break;
        case TransportType.Passenger:
            break;
        case TransportType.Special:
            break;
    }
    this.modalService.close();
  }
}

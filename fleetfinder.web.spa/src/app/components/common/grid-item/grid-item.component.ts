import {Component, Input} from '@angular/core';
import {
  CargoTransportationKindConst,
  PassengerFacilitiesConst,
  PassengerTransportationKindConst,
  RegionConst
} from 'src/app/data/enums.data';
import {TransportType} from 'src/app/models/enums/transport/transport-type.enum';
import {cargoItems} from "../../../data/transport/cargo-items";
import {CargoType} from "../../../models/enums/transport/cargo/cargo-type.enum";
import {IGridItem} from "../../../models/interfaces/grid-item.interface";
import {CargoTransportApiService} from "../../../api/CargoTransport/cargo-transport.api.service";
import {namesRoute} from "../../../data/names-route";
import {Router} from "@angular/router";
import {specialItems} from "../../../data/transport/special-items";
import {SpecialType} from "../../../models/enums/transport/special/special-type.enum";
import {passengerItems} from "../../../data/transport/passenger-items";
import {PassengerType} from "../../../models/enums/transport/passenger/passenger-type.enum";
import {CargoTransportationKind} from "../../../models/enums/transport/cargo/cargo-transportation-kind";
import {
  PassengerTransportationKind
} from "../../../models/enums/transport/passenger/passenger-transportation-kind.enum";

@Component({
  selector: 'app-grid-item',
  templateUrl: './grid-item.component.html',
  styleUrls: ['./grid-item.component.scss']
})
export class GridItemComponent {
  @Input() item: IGridItem;
  @Input() type: TransportType;

  RegionConst = RegionConst;
  CargoTransportationKindConst = CargoTransportationKindConst;
  PassengerTransportationKindConst = PassengerTransportationKindConst;
  PassengerFacilitiesConst = PassengerFacilitiesConst;
  TransportType = TransportType;

  constructor(private cargoTransportService: CargoTransportApiService,
              private router: Router) {
  }

  onClick() {
    this.router.navigate([this.getRouteToView(), this.item.Id]);
  }

  getTypeImg(item: any){
    switch (this.type){
      case TransportType.Cargo:
        return  '../../../../assets/icons/transport/cargo/icon-cargo-' + cargoItems.find(x => x.Value as CargoType === item)?.Icon + '.svg';
      case TransportType.Passenger:
        return  '../../../../assets/icons/transport/passenger/icon-passenger-' +  passengerItems.find(x => x.Value as PassengerType === item)?.Icon + '.svg';
      case TransportType.Special:
        return  '../../../../assets/icons/transport/special/icon-' + specialItems.find(x => x.Value as SpecialType === item)?.Icon + '.png';
    }
  }

  getTypeName(item: any) {
    switch (this.type){
      case TransportType.Cargo:
        return cargoItems.find(x => x.Value as CargoType === item)?.Text;
      case TransportType.Passenger:
        return passengerItems.find(x => x.Value as PassengerType === item)?.Text;
      case TransportType.Special:
        return specialItems.find(x => x.Value as SpecialType === item)?.Text;
    }
  }

  getTransportationTypeName() : string {
    if (this.type === TransportType.Cargo)
      return CargoTransportationKindConst[this.item.TransportationKind as CargoTransportationKind]
    else if (this.type === TransportType.Passenger)
      return PassengerTransportationKindConst[this.item.TransportationKind as PassengerTransportationKind]
    return '';
  }

  getRouteToView(){
    switch (this.type){
      case TransportType.Cargo:
        return namesRoute.TRANSPORT_CARGO_VIEW;
      case TransportType.Passenger:
        return namesRoute.TRANSPORT_PASSENGER_VIEW;
      case TransportType.Special:
        return namesRoute.TRANSPORT_SPECIAL_VIEW;
    }
  }
}

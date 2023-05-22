import {Component, Input} from '@angular/core';
import {CargoTransportationKindConst, RegionConst } from 'src/app/data/enums.data';
import { TransportType } from 'src/app/models/enums/transport/transport-type.enum';
import {cargoItems} from "../../../data/transport/cargo-items";
import {CargoType} from "../../../models/enums/transport/cargo/cargo-type.enum";
import {IGridItem} from "../../../models/interfaces/grid-item.interface";
import {CargoTransportApiService} from "../../../api/CargoTransport/cargo-transport.api.service";
import {namesRoute} from "../../../data/names-route";
import {Router} from "@angular/router";
import {specialItems} from "../../../data/transport/special-items";
import {SpecialType} from "../../../models/enums/transport/special/special-type.enum";

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
  TransportType = TransportType;

  constructor(private cargoTransportService: CargoTransportApiService,
              private router: Router) {
  }

  onClick() {
    this.router.navigate([namesRoute.TRANSPORT_CARGO_VIEW, this.item.Id]);
  }

  getTypeImg(item: any){
    switch (this.type){
      case TransportType.Cargo:
        return  '../../../../assets/icons/transport/cargo/icon-cargo-' + cargoItems.find(x => x.Value as CargoType === item)?.Icon + '.svg';
      case TransportType.Passenger:
        return  '../../../../assets/icons/transport/passenger/icon-passenger-' + item.Icon + '.svg';
      case TransportType.Special:
        return  '../../../../assets/icons/transport/special/icon-' + specialItems.find(x => x.Value as SpecialType === item)?.Icon + '.png';
    }
  }

  getTypeName(item: any) {
    switch (this.type){
      case TransportType.Cargo:
        return cargoItems.find(x => x.Value as CargoType === item)?.Text;
      case TransportType.Passenger:
        return  ""
      case TransportType.Special:
        return specialItems.find(x => x.Value as CargoType === item)?.Text;
    }
  }
}

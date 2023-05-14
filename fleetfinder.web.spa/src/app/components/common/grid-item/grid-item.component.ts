import {Component, Input} from '@angular/core';
import {CargoTransportationKindConst, ExperienceWorkConst, RegionConst } from 'src/app/data/enums.data';
import { TransportType } from 'src/app/models/enums/transport/transport-type.enum';
import {cargoItems} from "../../../data/transport/cargo-items";
import {CargoType} from "../../../models/enums/transport/cargo/cargo-type.enum";
import {IGridItem} from "../../../models/interfaces/grid-item.interface";

@Component({
  selector: 'app-grid-item',
  templateUrl: './grid-item.component.html',
  styleUrls: ['./grid-item.component.scss']
})
export class GridItemComponent {
  @Input() item: IGridItem;
  @Input() type: TransportType;

  RegionConst = RegionConst;
  ExperienceWorkConst = ExperienceWorkConst;
  CargoTransportationKindConst = CargoTransportationKindConst;
  TransportType = TransportType;

  currentTypeIcon(item: any, type: TransportType){
    this.type = type;
    switch (type){
      case TransportType.Cargo:
        return  '../../../../assets/icons/transport/cargo/icon-cargo-' + cargoItems.find(x => x.Value as CargoType === item)?.Icon + '.svg';
      case TransportType.Passenger:
        return  '../../../../assets/icons/transport/passenger/icon-passenger-' + item.Icon + '.svg';
      case TransportType.Special:
        return  '../../../../assets/icons/transport/special/icon-' + item.Icon + '.png';
    }
  }
}

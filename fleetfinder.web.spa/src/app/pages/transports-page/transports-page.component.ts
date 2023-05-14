import {Component, OnInit} from '@angular/core';
import {TransportType} from "../../models/enums/transport/transport-type.enum";
import {CargoTransportApiService} from "../../api/CargoTransport/cargo-transport.api.service";
import {CargoTransportSortParameter} from "../../models/enums/transport/cargo/cargo-transport-sort-parameter.enum";
import {DropdownItemModel} from "../../models/dropdown-item.model";
import {SortModel} from "../../models/sort.model";
import {CargoTransportItem} from "../../api/Common/Transport/CargoTransportItem";
import {CargoTransportGetListRequestDto} from "../../api/CargoTransport/get-list.models";
import {CargoTransportationKindConst, ExperienceWorkConst, RegionConst} from "../../data/enums.data";
import {IInfoBoxTransport} from "../../models/interfaces/info-box-transport.interface";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";
import {cargoItems} from "../../data/transport/cargo-items";
import {IGridItem} from "../../models/interfaces/grid-item.interface";

@Component({
  selector: 'app-transports-page',
  templateUrl: './transports-page.component.html',
  styleUrls: ['./transports-page.component.scss']
})
export class TransportsPageComponent implements OnInit{
  currentTab: TransportType = TransportType.Cargo;
  tab = TransportType;

  searchTerm: string = "";
  sortParameters: DropdownItemModel<SortModel<CargoTransportSortParameter>>[] = [
    { Value: new SortModel(CargoTransportSortParameter.Default, false), Preview: "По дате создания (в)" },
    { Value: new SortModel(CargoTransportSortParameter.Default, true), Preview: "По дате создания (у)" },
    { Value: new SortModel(CargoTransportSortParameter.PricePerHour, false), Preview: "По цене за час (в)" },
    { Value: new SortModel(CargoTransportSortParameter.PricePerHour, true), Preview: "По цене за час (у)" },
    { Value: new SortModel(CargoTransportSortParameter.PricePerShift, false), Preview: "По цене за смену (в)" },
    { Value: new SortModel(CargoTransportSortParameter.PricePerShift, true), Preview: "По цене за смену (у)" },
    { Value: new SortModel(CargoTransportSortParameter.PricePerKm, false) , Preview: "По цене  за километр (в)" },
    { Value: new SortModel(CargoTransportSortParameter.PricePerKm, true) , Preview: "По цене  за километр (у)" },
  ]
  sortParameter = this.sortParameters[0];

  items : IGridItem[] | null = null;
  totalCount : number = 0;
  RegionConst = RegionConst;
  ExperienceWorkConst = ExperienceWorkConst;
  CargoTransportationKindConst = CargoTransportationKindConst;
  TransportType = TransportType;
  currentType: TransportType | null = null;
  isLoad = false;

  constructor(private cargoTransportApiService: CargoTransportApiService) {
  }

  ngOnInit(): void {
    this.getListRequest();
  }

  getListRequest(){
    this.isLoad = true;
    this.items = null;
    let request = new CargoTransportGetListRequestDto();
    request.filter.TitleFilter = this.searchTerm;
    request.sortParameter = this.sortParameter.Value.Parameter;
    request.sortDesc = this.sortParameter.Value.ByDesc;
    this.cargoTransportApiService.getList(request)
      .subscribe(res => {
        this.items = res.Items;
        this.totalCount = res.TotalCount;
        this.isLoad = false;
      });
  }

  changeTab(tab: TransportType) {
    this.currentTab = tab;
  }

  onSelect(item: DropdownItemModel<any>) {
    this.sortParameter = item;
    this.getListRequest();
  }

  currentTypeIcon(item: any, type: TransportType){
    this.currentType = type;
    switch (type){
      case TransportType.Cargo:
        return  '../../../assets/icons/transport/cargo/icon-cargo-' + cargoItems.find(x => x.Value as CargoType === item)?.Icon + '.svg';
      case TransportType.Passenger:
        return  '../../../assets/icons/transport/passenger/icon-passenger-' + item.Icon + '.svg';
      case TransportType.Special:
        return  '../../../assets/icons/transport/special/icon-' + item.Icon + '.png';
    }
  }
}

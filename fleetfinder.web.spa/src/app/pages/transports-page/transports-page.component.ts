import {Component, OnInit} from '@angular/core';
import {TransportType} from "../../models/enums/transport/transport-type.enum";
import {CargoTransportApiService} from "../../api/CargoTransport/cargo-transport.api.service";
import {CargoTransportSortParameter} from "../../models/enums/transport/cargo/cargo-transport-sort-parameter.enum";
import {DropdownItemModel} from "../../models/dropdown-item.model";
import {SortModel} from "../../models/sort.model";
import {CargoTransportGetListRequestDto} from "../../api/CargoTransport/get-list.models";
import {IGridItem} from "../../models/interfaces/grid-item.interface";
import {FormControl} from "@angular/forms";
import {PaginationValue} from "../../components/ui/pagination/pagination.component";
import {getCargoTypeItems, getRegionItems} from "../../data/dropdown-items.data";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";
import {TransportFilter} from "../../models/transport/transport-filter.model";
import {Region} from "../../models/enums/common/region.enum";

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
  filterCargoForm : TransportFilter<CargoType> = new TransportFilter()

  items : IGridItem[] | null = null;
  totalCount : number = 0;
  TransportType = TransportType;
  isLoad = false;

  public pagination = { page: 1, pageSize: 6, total: 0 };


  CargoTypeItems = getCargoTypeItems(true);
  RegionItems = getRegionItems(true);

  constructor(private cargoTransportApiService: CargoTransportApiService) {
  }

  ngOnInit(): void {
    this.getListRequest();
  }

  getListRequest(){
    this.isLoad = true;
    this.items = null;
    let request = new CargoTransportGetListRequestDto();
    request.skipCount = this.pagination.page * this.pagination.pageSize - this.pagination.pageSize;
    request.filter = this.filterCargoForm;
    request.filter.TitleFilter = this.searchTerm;
    request.sortParameter = this.sortParameter.Value.Parameter;
    request.sortDesc = this.sortParameter.Value.ByDesc;
    this.cargoTransportApiService.getList(request)
      .subscribe(res => {
        this.items = res.Items;
        this.totalCount = res.TotalCount;
        this.pagination = {...this.pagination, total: this.totalCount}
        this.isLoad = false;
      });
  }

  changeTab(tab: TransportType) {
    this.currentTab = tab;
  }

  onSelect(item: DropdownItemModel<any>) {
    this.sortParameter = item;
    this.setDefaultPagination();
    this.getListRequest();
  }

  public onPageChange(pagination: PaginationValue): void {
    this.pagination = pagination;
    this.getListRequest();
  }

  setDefaultPagination(){
    this.pagination = { page: 1, pageSize: 6, total: 0 };
  }

  defaultTypeFilter = this.CargoTypeItems.find(x => x.Value == null) ?? null;
  defaultRegionFilter = this.RegionItems.find(x => x.Value == null) ?? null;
  onSelectTypeFilter(item: DropdownItemModel<CargoType>){
    this.filterCargoForm.TypeFilter = item.Value;
    this.getListRequest();
  }
  onSelectRegionFilter(item: DropdownItemModel<Region>){
    this.filterCargoForm.RegionFilter = item.Value;
    this.getListRequest();
  }

  resetFilter() {
    this.defaultTypeFilter = this.CargoTypeItems.find(x => x.Value == null) ?? null;
    this.filterCargoForm.TypeFilter = null;
    this.defaultRegionFilter = this.RegionItems.find(x => x.Value == null) ?? null;
    this.filterCargoForm.RegionFilter = null;
    this.getListRequest();
  }
}

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
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {ActivatedRoute, Router} from "@angular/router";
import {TransportService} from "../../services/transport.service";

@Component({
  selector: 'app-transports-page',
  templateUrl: './transports-page.component.html',
  styleUrls: ['./transports-page.component.scss']
})
export class TransportsPageComponent implements OnInit{
  //#region Consts
  currentTab: TransportType = TransportType.Cargo;
  tab = TransportType;
  TransportType = TransportType;
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
  CargoTypeItems = getCargoTypeItems(true);
  RegionItems = getRegionItems(true);
  //#endregion
  searchTerm: string = "";
  titlePage = "Поиск транспорта";
  sortParameter = this.sortParameters[0];
  filterCargoForm : TransportFilter<CargoType> = new TransportFilter()
  items : IGridItem[] | null = null;
  totalCount : number = 0;
  isLoad = false;
  pagination = { page: 1, pageSize: 6, total: 0 };

  valueDropdowns = {
    TypeFilter: this.CargoTypeItems.find(x => x.Value == null) ?? null,
    RegionFilter: this.RegionItems.find(x => x.Value == null) ?? null,
  }

  constructor(private cargoTransportApiService: CargoTransportApiService,
              private identifyService: IdentifyApiService,
              private router: Router,
              private transportService: TransportService) {
  }

  ngOnInit(): void {
    this.setUserFilter();
    this.getListRequest(this.transportService.getListRequest(this.router.url));
  }

  getListRequest(request : CargoTransportGetListRequestDto | null = null){
    this.isLoad = true;
    this.items = null;
    const isPreloadRequest = request != null;
    if (request == null){
      request = new CargoTransportGetListRequestDto;
      request.skipCount = this.pagination.page * this.pagination.pageSize - this.pagination.pageSize;
      request.filter = this.filterCargoForm;
      request.filter.TitleFilter = this.searchTerm;
      request.sortParameter = this.sortParameter.Value.Parameter;
      request.sortDesc = this.sortParameter.Value.ByDesc;
      this.transportService.saveListRequest(this.router.url, request);
    }
    else {
      this.valueDropdowns.TypeFilter = this.CargoTypeItems.find(x => x.Value == request?.filter.TypeFilter) ?? null;
      this.valueDropdowns.RegionFilter = this.RegionItems.find(x => x.Value == request?.filter.RegionFilter) ?? null;
      this.sortParameter = this.sortParameters.find(x => x.Value.Parameter == request?.sortParameter && x.Value.ByDesc == request?.sortDesc) ?? this.sortParameters[0];
      this.searchTerm = request.filter.TitleFilter ?? '';
    }
    this.cargoTransportApiService.getList(request)
      .subscribe(res => {
        this.items = res.Items;
        this.totalCount = res.TotalCount;
        this.pagination = {...this.pagination, total: this.totalCount}
        if (isPreloadRequest && request?.skipCount && request.skipCount > 0)
          this.pagination = {...this.pagination, page: Math.ceil(request.skipCount / this.pagination.pageSize) + 1}
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
  onSelectTypeFilter(item: DropdownItemModel<CargoType>){
    if (item instanceof Event) return;
    this.filterCargoForm.TypeFilter = item.Value;
    this.valueDropdowns.TypeFilter = item;
    this.resetPagination();
    this.getListRequest();
  }
  onSelectRegionFilter(item: DropdownItemModel<Region>){
    if (item instanceof Event) return;
    this.filterCargoForm.RegionFilter = item.Value;
    this.valueDropdowns.RegionFilter = item;
    this.resetPagination();
    this.getListRequest();
  }

  resetFilter() {
    this.valueDropdowns.TypeFilter = this.CargoTypeItems.find(x => x.Value == null) ?? null;
    this.filterCargoForm.TypeFilter = null;
    this.valueDropdowns.RegionFilter = this.RegionItems.find(x => x.Value == null) ?? null;
    this.filterCargoForm.RegionFilter = null;
    this.getListRequest();
  }

  setUserFilter() {
    if (this.router.url.includes('my')){
      this.filterCargoForm.UserFilter = this.identifyService.claims?.Id ?? null;
      this.titlePage = "Ваш транспорт";
    }
  }

  resetPagination() {
    this.pagination = { page: 1, pageSize: 6, total: 0 };
  }

  resetRequest() {
    this.searchTerm = "";
    this.sortParameter = this.sortParameters[0];
    this.resetPagination();
    this.resetFilter();
  }

  get countFilters() : number {
    return Object.values(this.valueDropdowns).filter(x => x?.Value != null).length;
  }
}

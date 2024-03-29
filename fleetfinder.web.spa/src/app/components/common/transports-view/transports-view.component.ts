import {Component, Input, OnInit} from '@angular/core';
import {DropdownItemModel} from "../../../models/dropdown-item.model";
import {
  getCargoTypeItems,
  getPassengerTypeItems,
  getRegionItems,
  getSortParameters,
  getSpecialTypeItems
} from "../../../data/dropdown-items.data";
import {TransportFilter} from "../../../models/transport/transport-filter.model";
import {IGridItem} from "../../../models/interfaces/grid-item.interface";
import { TransportType } from 'src/app/models/enums/transport/transport-type.enum';
import {CargoTransportGetListRequestDto} from "../../../api/CargoTransport/get-list.models";
import {PaginationValue} from "../../ui/pagination/pagination.component";
import {Region} from "../../../models/enums/common/region.enum";
import {CargoTransportApiService} from "../../../api/CargoTransport/cargo-transport.api.service";
import {IdentifyApiService} from "../../../api/Identify/identify.api.service";
import {Router} from "@angular/router";
import {TransportService} from "../../../services/transport.service";
import {SpecialTransportGetListRequestDto} from "../../../api/SpecialTransport/get-list.models";
import {SpecialTransportApiService} from "../../../api/SpecialTransport/special-transport.api.service";
import {PassengerTransportGetListRequestDto} from "../../../api/PassengerTransport/get-list.models";
import {PassengerTransportApiService} from "../../../api/PassengerTransport/passenger-transport.api.service";

interface ValueDropdowns {
  TypeFilter: DropdownItemModel<any | null> | null;
  RegionFilter: DropdownItemModel<Region | null> | null
}

@Component({
  selector: 'app-transports-view',
  templateUrl: './transports-view.component.html',
  styleUrls: ['./transports-view.component.scss']
})
export class TransportsViewComponent implements OnInit{
  @Input() type: TransportType;

  //#region Consts
  TransportType = TransportType;
  sortParameters = getSortParameters();
  CargoTypeItems = getCargoTypeItems();
  PassengerTypeItems = getPassengerTypeItems();
  SpecialTypeItems = getSpecialTypeItems();
  RegionItems = getRegionItems();
  //#endregion
  searchTerm: string = "";
  sortParameter = this.sortParameters[0];
  filterCargoForm : TransportFilter<any> = new TransportFilter()
  items : IGridItem[] | null = null;
  totalCount : number = 0;
  isLoad = false;
  pagination = { page: 1, pageSize: 6, total: 0 };
  currentTypeItems: DropdownItemModel<any | null>[] = [];
  valueDropdowns: ValueDropdowns = {
    TypeFilter: null,
    RegionFilter: this.RegionItems.find(x => x.Value == null) ?? null,
  }

  constructor(private cargoTransportApiService: CargoTransportApiService,
              private specialTransportApiService: SpecialTransportApiService,
              private passengerTransportApiService: PassengerTransportApiService,
              private identifyService: IdentifyApiService,
              private router: Router,
              private transportService: TransportService) {

  }

  ngOnInit(): void {
    this.initPage();
    this.setUserFilter();
    this.getListItems(true);
  }


  getListItems(preload: boolean = false){
    this.isLoad = true;
    this.items = null;
    switch (this.type){
      case TransportType.Cargo:
        this.getCargoListRequest(preload ? this.transportService.getCargoListRequest(this.router.url) : null);
        break;
      case TransportType.Passenger:
        this.getPassengerListRequest(preload ? this.transportService.getPassengerListRequest(this.router.url) : null);
        break;
      case TransportType.Special:
        this.getSpecialListRequest(preload ? this.transportService.getSpecialListRequest(this.router.url) : null);
        break;
    }
  }

  getCargoListRequest(request : CargoTransportGetListRequestDto | null = null){
    const isPreloadRequest = request != null;
    if (request == null){
      request = new CargoTransportGetListRequestDto();
      request.skipCount = this.pagination.page * this.pagination.pageSize - this.pagination.pageSize;
      request.filter = this.filterCargoForm;
      request.filter.TitleFilter = this.searchTerm;
      request.sortParameter = this.sortParameter.Value.Parameter;
      request.sortDesc = this.sortParameter.Value.ByDesc;
      this.transportService.saveListRequest(this.router.url, request);
    }
    else
      this.setValuesRequest(request);

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

  getPassengerListRequest(request: PassengerTransportGetListRequestDto | null = null){
    const isPreloadRequest = request != null;
    if (request == null){
      request = new PassengerTransportGetListRequestDto();
      request.skipCount = this.pagination.page * this.pagination.pageSize - this.pagination.pageSize;
      request.filter = this.filterCargoForm;
      request.filter.TitleFilter = this.searchTerm;
      request.sortParameter = this.sortParameter.Value.Parameter;
      request.sortDesc = this.sortParameter.Value.ByDesc;
      this.transportService.saveListRequest(this.router.url, request);
    }
    else
      this.setValuesRequest(request);

    this.passengerTransportApiService.getList(request)
      .subscribe(res => {
        this.items = res.Items;
        this.totalCount = res.TotalCount;
        this.pagination = {...this.pagination, total: this.totalCount}
        if (isPreloadRequest && request?.skipCount && request.skipCount > 0)
          this.pagination = {...this.pagination, page: Math.ceil(request.skipCount / this.pagination.pageSize) + 1}
        this.isLoad = false;
      });
  }

  getSpecialListRequest(request : SpecialTransportGetListRequestDto | null = null) {
    const isPreloadRequest = request != null;
    if (request == null){
      request = new SpecialTransportGetListRequestDto();
      request.skipCount = this.pagination.page * this.pagination.pageSize - this.pagination.pageSize;
      request.filter = this.filterCargoForm;
      request.filter.TitleFilter = this.searchTerm;
      request.sortParameter = this.sortParameter.Value.Parameter;
      request.sortDesc = this.sortParameter.Value.ByDesc;
      this.transportService.saveListRequest(this.router.url, request);
    }
    else
      this.setValuesRequest(request);

    this.specialTransportApiService.getList(request)
      .subscribe(res => {
        this.items = res.Items;
        this.totalCount = res.TotalCount;
        this.pagination = {...this.pagination, total: this.totalCount}
        if (isPreloadRequest && request?.skipCount && request.skipCount > 0)
          this.pagination = {...this.pagination, page: Math.ceil(request.skipCount / this.pagination.pageSize) + 1}
        this.isLoad = false;
      });
  }

  setValuesRequest(request: any ){
    this.valueDropdowns.TypeFilter = this.CargoTypeItems.find(x => x.Value == request?.filter.TypeFilter) ?? null;
    this.valueDropdowns.RegionFilter = this.RegionItems.find(x => x.Value == request?.filter.RegionFilter) ?? null;
    this.sortParameter = this.sortParameters.find(x => x.Value.Parameter == request?.sortParameter && x.Value.ByDesc == request?.sortDesc) ?? this.sortParameters[0];
    this.searchTerm = request.filter.TitleFilter ?? '';
  }

  initPage(){
    switch (this.type){
      case TransportType.Cargo:
        this.valueDropdowns.TypeFilter = this.CargoTypeItems.find(x => x.Value == null) ?? null
        this.currentTypeItems = this.CargoTypeItems;
        break;
      case TransportType.Passenger:
        this.valueDropdowns.TypeFilter = this.PassengerTypeItems.find(x => x.Value == null) ?? null
        this.currentTypeItems = this.PassengerTypeItems;
        break;
      case TransportType.Special:
        this.valueDropdowns.TypeFilter = this.SpecialTypeItems.find(x => x.Value == null) ?? null
        this.currentTypeItems = this.SpecialTypeItems;
        break;
    }
  }

  onSelect(item: DropdownItemModel<any>) {
    this.sortParameter = item;
    this.setDefaultPagination();
    this.getListItems();
  }

  public onPageChange(pagination: PaginationValue): void {
    this.pagination = pagination;
    this.getListItems();
  }

  setDefaultPagination(){
    this.pagination = { page: 1, pageSize: 6, total: 0 };
  }
  onSelectTypeFilter(item: DropdownItemModel<any>){
    if (item instanceof Event) return;
    this.filterCargoForm.TypeFilter = item.Value;
    this.valueDropdowns.TypeFilter = item;
    this.resetPagination();
    this.getListItems();
  }
  onSelectRegionFilter(item: DropdownItemModel<Region>){
    if (item instanceof Event) return;
    this.filterCargoForm.RegionFilter = item.Value;
    this.valueDropdowns.RegionFilter = item;
    this.resetPagination();
    this.getListItems();
  }

  resetFilter() {
    this.valueDropdowns.TypeFilter = this.CargoTypeItems.find(x => x.Value == null) ?? null;
    this.filterCargoForm.TypeFilter = null;
    this.valueDropdowns.RegionFilter = this.RegionItems.find(x => x.Value == null) ?? null;
    this.filterCargoForm.RegionFilter = null;
    this.getListItems();
  }

  setUserFilter() {
    if (this.router.url.includes('my')){
      this.filterCargoForm.UserFilter = this.identifyService.claims?.Id ?? null;
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

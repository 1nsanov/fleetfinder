import {Component, Input, OnInit} from '@angular/core';
import {DropdownItemModel} from "../../../models/dropdown-item.model";
import {getCargoTypeItems, getRegionItems, getSortParameters, getSpecialItems} from "../../../data/dropdown-items.data";
import {TransportFilter} from "../../../models/transport/transport-filter.model";
import {CargoType} from "../../../models/enums/transport/cargo/cargo-type.enum";
import {IGridItem} from "../../../models/interfaces/grid-item.interface";
import { TransportType } from 'src/app/models/enums/transport/transport-type.enum';
import {CargoTransportGetListRequestDto} from "../../../api/CargoTransport/get-list.models";
import {PaginationValue} from "../../ui/pagination/pagination.component";
import {Region} from "../../../models/enums/common/region.enum";
import {CargoTransportApiService} from "../../../api/CargoTransport/cargo-transport.api.service";
import {IdentifyApiService} from "../../../api/Identify/identify.api.service";
import {Router} from "@angular/router";
import {TransportService} from "../../../services/transport.service";

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
  SpecialTypeItems = getSpecialItems();
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
              private identifyService: IdentifyApiService,
              private router: Router,
              private transportService: TransportService) {

  }

  ngOnInit(): void {
    this.initPage();
    this.setUserFilter();
    this.getListItems();
  }

  getCargoListRequest(request : CargoTransportGetListRequestDto | null = null){
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

  getSpecialItems(request : any | null = null) {
    this.isLoad = true;
    this.items = null;
    this.isLoad = false;
  }

  getListItems(preload: boolean = false){
    switch (this.type){
      case TransportType.Cargo:
        this.getCargoListRequest(preload ? this.transportService.getCargoListRequest(this.router.url) : null);
        break;
      case TransportType.Passenger:
        break;
      case TransportType.Special:
        this.getSpecialItems(preload ? this.transportService.getSpecialListRequest(this.router.url) : null);
        break;
    }
  }

  initPage(){
    switch (this.type){
      case TransportType.Cargo:
        this.valueDropdowns.TypeFilter = this.CargoTypeItems.find(x => x.Value == null) ?? null
        this.currentTypeItems = this.CargoTypeItems;
        break;
      case TransportType.Passenger:
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

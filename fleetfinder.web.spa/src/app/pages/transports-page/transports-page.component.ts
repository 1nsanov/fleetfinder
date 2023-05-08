import {Component, OnInit} from '@angular/core';
import {TransportType} from "../../models/enums/transport/transport-type.enum";
import {CargoTransportApiService} from "../../api/CargoTransport/cargo-transport.api.service";
import {CargoTransportDto, CargoTransportGetListRequestDto} from "../../api/CargoTransport/cargo-transport.api.models";
import {CargoTransportSortParameter} from "../../models/enums/transport/cargo/cargo-transport-sort-parameter.enum";
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
  items : CargoTransportDto[] | null = null;
  totalCount : number = 0;

  constructor(private cargoTransportApiService: CargoTransportApiService) {
  }

  ngOnInit(): void {
    this.getListRequest();
  }

  getListRequest(){
    this.items = null;
    let request = new CargoTransportGetListRequestDto();
    if (this.searchTerm)
      request.filter.TitleFilter = this.searchTerm;
    this.cargoTransportApiService.getList(request)
      .subscribe(res => {
        this.items = res.Items;
        this.totalCount = res.TotalCount;
      });
  }

  changeTab(tab: TransportType) {
    this.currentTab = tab;
  }
}

import {Component, OnInit} from '@angular/core';
import {TransportType} from "../../models/enums/transport/transport-type.enum";
import {CargoTransportApiService} from "../../api/CargoTransport/cargo-transport.api.service";
import {TransportSortParameter} from "../../models/enums/transport/cargo/cargo-transport-sort-parameter.enum";
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
import {ActivatedRoute, NavigationEnd, Router} from "@angular/router";
import {TransportService} from "../../services/transport.service";
import {namesRoute} from "../../data/names-route";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-transports-page',
  templateUrl: './transports-page.component.html',
  styleUrls: ['./transports-page.component.scss']
})
export class TransportsPageComponent implements OnInit{
  currentTab: TransportType | null = null;
  tab = TransportType;
  private routerSubscription: Subscription = new Subscription();
  namesRoute = namesRoute;
  titlePage = "Поиск транспорта";
  rootPath: string = "";

  constructor(private router: Router,) {
  }

  ngOnInit(): void {
    this.setTitlePage();
    this.changeTab(this.router.url)
    this.routerSubscription = this.router.events.subscribe( (event) => {
      if (event instanceof NavigationEnd) {
        this.changeTab(event.url);
      }
    });
  }

  changeTab(url: string) {
    this.currentTab = this.getTabByRoute(url);
  }

  routeTo(childPath: string){
    this.router.navigate([`${this.rootPath}/${childPath}`])
  }

  getTabByRoute(route: string){
    switch (route){
      case `${this.rootPath}/${namesRoute.TRANSPORTS_CARGO}`:
        return TransportType.Cargo;
      case `${this.rootPath}/${namesRoute.TRANSPORTS_PASSENGER}`:
        return TransportType.Passenger;
      case `${this.rootPath}/${namesRoute.TRANSPORTS_SPECIAL}`:
        return TransportType.Special;
      default:
        return null;
    }
  }

  setTitlePage() {
    if (this.router.url.includes('my')){
      this.rootPath = `/${namesRoute.TRANSPORTS_MY}`
      this.titlePage = "Ваш транспорт";
    }
    else {
      this.rootPath = `/${namesRoute.TRANSPORTS}`
      this.titlePage = "Поиск транспорта";
    }
  }
}

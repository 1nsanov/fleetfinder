import { Component } from '@angular/core';
import {DropdownItemModel} from "../../models/dropdown-item.model";
import {TransportType} from "../../models/enums/transport/transport-type.enum";

@Component({
  selector: 'app-transports-page',
  templateUrl: './transports-page.component.html',
  styleUrls: ['./transports-page.component.scss']
})
export class TransportsPageComponent {
  currentTab: TransportType = TransportType.Cargo;
  tab = TransportType;

  searchTerm: string = "";

  changeTab(tab: TransportType) {
    this.currentTab = tab;
  }
}

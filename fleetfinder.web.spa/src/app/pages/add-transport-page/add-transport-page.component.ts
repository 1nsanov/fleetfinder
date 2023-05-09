import { Component } from '@angular/core';
import {
  getCargoBodyKindItems, getCargoTransportationKindItems,
  getExperienceWorkItems,
  getPaymentMethodItems,
  getPaymentOrderItems,
  getRegionItems
} from "../../data/dropdown-items.data";

@Component({
  selector: 'app-add-transport-page',
  templateUrl: './add-transport-page.component.html',
  styleUrls: ['./add-transport-page.component.scss']
})
export class AddTransportPageComponent {
  RegionItems = getRegionItems();
  ExperienceWorkItems = getExperienceWorkItems();
  PaymentMethodItems = getPaymentMethodItems();
  PaymentOrderItems = getPaymentOrderItems();
  CargoBodyKindItems = getCargoBodyKindItems();
  CargoTransportationKindItems = getCargoTransportationKindItems();
}

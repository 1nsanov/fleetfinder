import { Component } from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {NotificationService} from "../../services/notification.service";
import {cargoItems} from "../../data/transport/cargo-items";
import {passengerItems} from "../../data/transport/passenger-items";
import {specialItems} from "../../data/transport/special-items";

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent {
  cargo = cargoItems;
  passenger = passengerItems;
  special = specialItems;
}

import { Component } from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {NotificationService} from "../../services/notification.service";
import {cargoItems} from "../../data/transport/cargo-items";
import {passengerItems} from "../../data/transport/passenger-items";
import {specialItems} from "../../data/transport/special-items";
import {Router} from "@angular/router";
import {namesRoute} from "../../data/names-route";

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent {
  cargo = cargoItems;
  passenger = passengerItems;
  special = specialItems;

  constructor(private router: Router) {
  }

  routeToTransports(){
    this.router.navigate([`/${namesRoute.TRANSPORTS}`])
  }
}

import { Component } from '@angular/core';
import {environment} from "../environments/environment";
import {GlobalLoaderService} from "./services/global-loader.service";
import {IdentifyApiService} from "./api/Identify/identify.api.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public loaderService: GlobalLoaderService,
              private identifyService: IdentifyApiService) {
    identifyService.getClaims().subscribe(() => {
      const name = identifyService.claims?.FullName;
      if (name)
        loaderService.changeTitle(`Приветcтвуем вас, <br>${name}`);
      else
        setTimeout(() => loaderService.stop(), 250);
    }, error => setTimeout(() => loaderService.stop(), 250))
  }
  title = 'FLEETFINDER';
}

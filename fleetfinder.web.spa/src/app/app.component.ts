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
  baseUrl: string = environment.apiUrl;
  constructor(public loaderService: GlobalLoaderService,
              private identifyService: IdentifyApiService) {
    identifyService.getClaims().subscribe(() => {
      setTimeout(() => loaderService.stop(), 200);
    }, error => setTimeout(() => loaderService.stop(), 200))
  }
  title = 'FLEETFINDER';
}

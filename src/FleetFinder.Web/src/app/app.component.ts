import { Component, ChangeDetectionStrategy } from '@angular/core';
import {GlobalLoaderService} from "./services/global-loader.service";
import {IdentityApiService} from "./api/Identity/identity.api.service";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    changeDetection: ChangeDetectionStrategy.Eager,
    standalone: false
})
export class AppComponent {
  constructor(public loaderService: GlobalLoaderService,
              private identifyService: IdentityApiService) {
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

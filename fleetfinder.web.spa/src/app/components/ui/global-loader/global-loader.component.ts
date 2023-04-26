import { Component } from '@angular/core';
import {GlobalLoaderService} from "../../../services/global-loader.service";
import {IdentifyApiService} from "../../../api/Identify/identify.api.service";

@Component({
  selector: 'app-global-loader',
  templateUrl: './global-loader.component.html',
  styleUrls: ['./global-loader.component.scss']
})
export class GlobalLoaderComponent {
  loading = true;
  isAnimationTitle = false;
  constructor(public loaderService: GlobalLoaderService,
              public identifyService: IdentifyApiService) {
    this.loaderService.loading$.subscribe(() => {
      setTimeout(() => this.loading = this.loaderService.loading$.value, 500)
    })

    this.loaderService.title$.subscribe(() => {
      this.isAnimationTitle = loaderService.title$.value.includes('вас');
    })
  }
}

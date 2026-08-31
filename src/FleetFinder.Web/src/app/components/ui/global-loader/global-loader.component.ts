import { Component, ChangeDetectionStrategy } from '@angular/core';
import {GlobalLoaderService} from "../../../services/global-loader.service";
import {IdentityApiService} from "../../../api/Identity/identity.api.service";

@Component({
    selector: 'app-global-loader',
    templateUrl: './global-loader.component.html',
    styleUrls: ['./global-loader.component.scss'],
    changeDetection: ChangeDetectionStrategy.Eager,
    standalone: false
})
export class GlobalLoaderComponent {
  loading = true;
  isAnimationTitle = false;
  constructor(public loaderService: GlobalLoaderService,
              public identifyService: IdentityApiService) {
    this.loaderService.loading$.subscribe(() => {
      setTimeout(() => this.loading = this.loaderService.loading$.value, 500)
    })

    this.loaderService.title$.subscribe(() => {
      this.isAnimationTitle = loaderService.title$.value.includes('вас');
    })
  }
}

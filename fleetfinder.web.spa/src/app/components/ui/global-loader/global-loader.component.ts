import { Component } from '@angular/core';
import {GlobalLoaderService} from "../../../services/global-loader.service";

@Component({
  selector: 'app-global-loader',
  templateUrl: './global-loader.component.html',
  styleUrls: ['./global-loader.component.scss']
})
export class GlobalLoaderComponent {
  loading = true;
  constructor(public loaderService: GlobalLoaderService) {
    this.loaderService.loading$.subscribe(state => {
      setTimeout(() => this.loading = this.loaderService.loading$.value, 500)
    })
  }
}

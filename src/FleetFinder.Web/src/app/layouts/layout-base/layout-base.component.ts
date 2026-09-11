import { Component, ChangeDetectionStrategy } from '@angular/core';
import { Router } from "@angular/router";
import {GlobalLoaderService} from "../../services/global-loader.service";

@Component({
    selector: 'app-layout-base',
    templateUrl: './layout-base.component.html',
    styleUrls: ['./layout-base.component.scss'],
    changeDetection: ChangeDetectionStrategy.Eager,
    standalone: false
})
export class LayoutBaseComponent {
  constructor(private router: Router) {
  }

  get isShowContainer () {
    return this.router.url !== '/sign-up' && this.router.url !== '/sign-in'
  }
}

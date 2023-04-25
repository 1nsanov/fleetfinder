import { Component } from '@angular/core';
import { Router } from "@angular/router";
import {GlobalLoaderService} from "../../services/global-loader.service";

@Component({
  selector: 'app-layout-base',
  templateUrl: './layout-base.component.html',
  styleUrls: ['./layout-base.component.scss']
})
export class LayoutBaseComponent {
  constructor(private router: Router,
              public loaderService: GlobalLoaderService) {
  }

  get isShowContainer () {
    return this.router.url !== '/sign-up' && this.router.url !== '/sign-in'
  }
}

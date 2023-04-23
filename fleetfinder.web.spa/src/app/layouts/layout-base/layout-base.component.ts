import { Component } from '@angular/core';
import { Router } from "@angular/router";

@Component({
  selector: 'app-layout-base',
  templateUrl: './layout-base.component.html',
  styleUrls: ['./layout-base.component.scss']
})
export class LayoutBaseComponent {
  constructor(private router: Router) {

  }

  get isShowContainer () {
    return this.router.url !== '/sign-up' && this.router.url !== '/sign-in'
  }
}

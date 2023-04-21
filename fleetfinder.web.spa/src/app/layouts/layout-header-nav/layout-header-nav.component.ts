import {Component, OnDestroy, OnInit} from '@angular/core';
import {navTab, NavTab} from "../../models/enums/nav-tab.enum";
import {NavigationEnd, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {namesRoute} from "../../models/namesRoute";

@Component({
  selector: 'app-layout-header-nav',
  templateUrl: './layout-header-nav.component.html',
  styleUrls: ['./layout-header-nav.component.scss']
})
export class LayoutHeaderNavComponent implements OnInit, OnDestroy{
  constructor(private router: Router) {
  }

  currentNavTab: NavTab | null = null;
  currentRoute: string = "";
  private routerSubscription: Subscription = new Subscription();
  navTab = navTab;
  HeaderNavEl : HTMLElement | null = null;
  BackgroundContentEl : HTMLElement | null = null;
  LayoutBaseEl : HTMLElement | null = null;
  HeaderPreviewEl: HTMLElement | null = null;

  ngOnInit(): void {
    this.routerSubscription = this.router.events.subscribe( (event) => {
      if (event instanceof NavigationEnd) {
        this.currentRoute = event.url;
      }
    });

    setTimeout(() => {
      this.currentNavTab = this.getNavTabByRoute();
      this.swapNavTab(this.currentNavTab);

      this.HeaderNavEl = document.getElementById("header-nav");
      this.BackgroundContentEl = document.getElementById("background-content")
      this.LayoutBaseEl = document.getElementById("layout-base");
      this.HeaderPreviewEl = document.getElementById("header-preview")

      if (!this.isHomeRoute) this.setFixNavHeader();
      this.fixedHeaderByScroll()
    }, 10)
  }

  changeNavTab(navTab: NavTab) {
    if (this.currentNavTab === navTab) return;
    window.scrollTo(0,0);
    this.swapNavTab(navTab)
    this.router.navigate([this.currentNavTab])
  }

  swapNavTab(navTab: NavTab){
    this.currentNavTab = navTab;
    setTimeout(() => {
      if (this.isHomeRoute){
        this.removeFixNavHeader();
        this.HeaderPreviewEl?.classList.remove('hidden-header-preview')
      }
      else{
        // this.setFixNavHeader();
        this.HeaderPreviewEl?.classList.add('hidden-header-preview')
      }
    },10)
  }

  getNavTabByRoute(){
    switch (this.currentRoute){
      case `/${namesRoute.home}`:
        return NavTab.Home;
      case `/${namesRoute.transports}`:
        return NavTab.Transports;
      case `/${namesRoute.orders}`:
        return NavTab.Orders
      case `/${namesRoute.about}`:
        return NavTab.About
      default:
        return NavTab.None
    }
  }

  fixedHeaderByScroll(){
    const self = this;
    window.onscroll = function() {
      if (self.isHomeRoute){
        if (window.pageYOffset > 236)
          self.setFixNavHeader()
        else
          self.removeFixNavHeader();
      }
      else
        self.setFixNavHeader()
    };
  }

  setFixNavHeader(){
    this.HeaderNavEl?.classList.add("fixed-header");
    this.BackgroundContentEl?.classList.add("margin-top-content")
  }

  removeFixNavHeader(){
    this.HeaderNavEl?.classList.remove("fixed-header");
    this.BackgroundContentEl?.classList.remove("margin-top-content")
  }

  get isHomeRoute(){
    return this.currentRoute === NavTab.Home;
  }

  ngOnDestroy() {
    this.routerSubscription.unsubscribe();
  }
}

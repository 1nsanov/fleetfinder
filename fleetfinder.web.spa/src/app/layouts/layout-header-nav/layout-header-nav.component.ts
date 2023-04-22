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

  isDisabledOnScroll: boolean = false;

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
    this.swapNavTab(navTab);
    this.scrollToTop();
    this.router.navigate([this.currentNavTab])
  }

  scrollToTop() {
    // Define the animation duration and easing function
    const duration = 300;
    const easing = (t: number) => t * t;

    // Define the starting and ending positions
    const start = window.scrollY || document.documentElement.scrollTop;
    const end = 0;

    // Define the animation function
    const animate = (time: number) => {
      const elapsed = time - start_time;
      const progress = Math.min(elapsed / duration, 1);
      const value = start + easing(progress) * (end - start);
      window.scrollTo(0, value);
      if (progress < 1) {
        requestAnimationFrame(animate);
      }
    };

    // Start the animation
    this.isDisabledOnScroll = this.currentNavTab !== NavTab.Home
    const start_time = performance.now();
    requestAnimationFrame(animate);
    setTimeout(() => this.isDisabledOnScroll = false, duration)
  }

  swapNavTab(navTab: NavTab){
    this.currentNavTab = navTab;
    setTimeout(() => {
      if (this.isHomeRoute){
        this.HeaderPreviewEl?.classList.remove('hidden-header-preview')
        this.removeFixNavHeader()
      }
      else{
        this.HeaderPreviewEl?.classList.add('hidden-header-preview')
      }
    },20)
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
      if (self.isDisabledOnScroll) return;
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

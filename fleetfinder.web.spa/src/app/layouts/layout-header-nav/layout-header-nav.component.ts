import {Component, HostListener, OnDestroy, OnInit} from '@angular/core';
import {navTab, NavTab} from "../../models/enums/nav-tab.enum";
import {NavigationEnd, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {namesRoute} from "../../data/names-route";
import {IdentifyApiService} from "../../api/Identify/identify.api.service";

@Component({
  selector: 'app-layout-header-nav',
  templateUrl: './layout-header-nav.component.html',
  styleUrls: ['./layout-header-nav.component.scss']
})
export class LayoutHeaderNavComponent implements OnInit, OnDestroy{
  constructor(private router: Router,
              public identifyService: IdentifyApiService) {
  }

  currentNavTab: NavTab | null = null;
  currentRoute: string = "";
  private routerSubscription: Subscription = new Subscription();
  navTab = navTab;
  namesRoute = namesRoute;
  HeaderNavEl : HTMLElement | null = null;
  BackgroundContentEl : HTMLElement | null = null;
  LayoutBaseEl : HTMLElement | null = null;
  HeaderPreviewEl: HTMLElement | null = null;

  isDisabledOnScroll: boolean = false;
  isLoadLogout = false;
  isOpenPopup = false;

  ngOnInit(): void {
    this.routerSubscription = this.router.events.subscribe( (event) => {
      if (event instanceof NavigationEnd) {
        this.currentRoute = event.url;
        const navTab = this.getNavTabByRoute()
        this.swapNavTab(navTab);
      }
    });

    setTimeout(() => {
      this.currentNavTab = this.getNavTabByRoute();
      this.swapNavTab(this.currentNavTab);

      this.HeaderNavEl = document.getElementById("header-nav");
      this.BackgroundContentEl = document.getElementById("background-content")
      this.LayoutBaseEl = document.getElementById("layout-base");
      this.HeaderPreviewEl = document.getElementById("header-preview")

      if (!this.isShowHeaderPreview) this.setFixNavHeader();
      this.fixedHeaderByScroll()
    }, 10)
  }

  //---Nav---

  changeNavTab(navTab: NavTab) {
    if (this.currentNavTab === navTab) return;
    this.swapNavTab(navTab);
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
    if (this.currentNavTab === navTab) return;
    this.currentNavTab = navTab;
    this.scrollToTop();
    setTimeout(() => {
      if (this.isShowHeaderPreview){
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
      case `/${namesRoute.HOME}`:
        return NavTab.Home;
      case `/${namesRoute.TRANSPORTS}`:
      case `/${namesRoute.TRANSPORTS}/${namesRoute.TRANSPORTS_CARGO}`:
      case `/${namesRoute.TRANSPORTS}/${namesRoute.TRANSPORTS_PASSENGER}`:
      case `/${namesRoute.TRANSPORTS}/${namesRoute.TRANSPORTS_SPECIAL}`:
        return NavTab.Transports;
      case `/${namesRoute.ORDERS}`:
        return NavTab.Orders
      case `/${namesRoute.ABOUT}`:
        return NavTab.About
      default:
        return NavTab.None
    }
  }

  fixedHeaderByScroll(){
    const self = this;
    window.onscroll = function() {
      if (self.isDisabledOnScroll) return;
      const pageYOffset = window.innerWidth >= 768 ? 235 : 175;
      if (self.isShowHeaderPreview && window.pageYOffset <= pageYOffset){
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

  get isShowHeaderPreview(){
    return this.currentRoute === NavTab.Home;
  }

  get isSign(){
    return this.currentRoute === `/${namesRoute.SIGN_UP}` || this.currentRoute === `/${namesRoute.SIGN_IN}`
  }

  //---Route---

  routePageSignUp() {
    this.swapNavTab(NavTab.None)
    this.router.navigate([`/${namesRoute.SIGN_UP}`]);
  }

  routePageSignIn () {
    this.swapNavTab(NavTab.None)
    this.router.navigate([`/${namesRoute.SIGN_IN}`]);
  }

  routeTo(path : string) {
    this.swapNavTab(NavTab.None)
    this.router.navigate([`/${path}`]);
  }

  logout() {
    this.isLoadLogout = true;
    this.identifyService.logout().subscribe();
  }


  ngOnDestroy() {
    this.routerSubscription.unsubscribe();
  }

  // Popup
  openPopup() {
    this.isOpenPopup = true;
  }
  closePopup() {
    this.isOpenPopup = false;
  }

  @HostListener('document:click', ['$event.target'])
  onClick(target: any) {
    if (target.classList.contains('i-c-p')) return;
    const isInsidePopup = target.closest('.popup-container');
    const isNavItem = target.classList.contains('__item-nav');
    if (!isInsidePopup || isNavItem) {
      this.closePopup();
    }
  }
}

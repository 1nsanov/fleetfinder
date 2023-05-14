import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {AppRoutingModule} from "./app-routing.module";
import { LayoutBaseComponent } from './layouts/layout-base/layout-base.component';
import { LayoutHeaderPreviewComponent } from './layouts/layout-header-preview/layout-header-preview.component';
import { LayoutHeaderNavComponent } from './layouts/layout-header-nav/layout-header-nav.component';
import { TransportsPageComponent } from './pages/transports-page/transports-page.component';
import { LandingPageComponent } from './pages/landing-page/landing-page.component';
import { OrdersPageComponent } from './pages/orders-page/orders-page.component';
import { AboutPageComponent } from './pages/about-page/about-page.component';
import { InputComponent } from './components/ui/input/input.component';
import { ButtonComponent } from './components/ui/button/button.component';
import { ModalWindowComponent } from './components/ui/modal-window/modal-window.component';
import { SignUpPageComponent } from './pages/sign-up-page/sign-up-page.component';
import { SignInPageComponent } from './pages/sign-in-page/sign-in-page.component';
import {TokenInterceptor} from "./interceptors/token.interceptor";
import {RedirectInterceptor} from "./interceptors/redirect.interceptor";
import {NgOptimizedImage} from "@angular/common";
import { GlobalLoaderComponent } from './components/ui/global-loader/global-loader.component';
import { NotificationComponent } from './components/ui/notification/notification.component';
import { DropdownComponent } from './components/ui/dropdown/dropdown.component';
import { AbbreviationNamePipe } from './pipes/abbreviation-name.pipe';
import { AddTransportPageComponent } from './pages/add-transport-page/add-transport-page.component';
import { PreloaderComponent } from './components/ui/preloader/preloader.component';
import { GridItemComponent } from './components/common/grid-item/grid-item.component';
import { GridItemsComponent } from './components/common/grid-items/grid-items.component';
import { ContactCardComponent } from './components/common/contact-card/contact-card.component';
import { PhonePipe } from './pipes/phone.pipe';

@NgModule({
  declarations: [
    AppComponent,
    LayoutBaseComponent,
    LayoutHeaderPreviewComponent,
    LayoutHeaderNavComponent,
    TransportsPageComponent,
    LandingPageComponent,
    OrdersPageComponent,
    AboutPageComponent,
    InputComponent,
    ButtonComponent,
    ModalWindowComponent,
    SignUpPageComponent,
    SignInPageComponent,
    GlobalLoaderComponent,
    NotificationComponent,
    DropdownComponent,
    AbbreviationNamePipe,
    AddTransportPageComponent,
    PreloaderComponent,
    GridItemComponent,
    GridItemsComponent,
    ContactCardComponent,
    PhonePipe,
  ],
    imports: [
        BrowserModule, HttpClientModule, AppRoutingModule, NgOptimizedImage,
    ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: RedirectInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

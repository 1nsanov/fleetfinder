import { HttpClientModule } from '@angular/common/http';
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

@NgModule({
  declarations: [
    AppComponent,
    LayoutBaseComponent,
    LayoutHeaderPreviewComponent,
    LayoutHeaderNavComponent,
    TransportsPageComponent,
    LandingPageComponent,
    OrdersPageComponent,
    AboutPageComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

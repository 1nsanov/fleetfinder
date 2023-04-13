import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {AppRoutingModule} from "./app-routing.module";
import { LayoutBaseComponent } from './layouts/layout-base/layout-base.component';
import { LayoutHeaderPreviewComponent } from './layouts/layout-header-preview/layout-header-preview.component';

@NgModule({
  declarations: [
    AppComponent,
    LayoutBaseComponent,
    LayoutHeaderPreviewComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

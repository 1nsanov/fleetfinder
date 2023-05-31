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
import { PreloaderComponent } from './components/ui/preloader/preloader.component';
import { GridItemComponent } from './components/common/grid-item/grid-item.component';
import { GridItemsComponent } from './components/common/grid-items/grid-items.component';
import { ContactCardComponent } from './components/common/contact-card/contact-card.component';
import { PhonePipe } from './pipes/phone.pipe';
import { PaginationComponent } from './components/ui/pagination/pagination.component';
import { PlugEmptyItemsComponent } from './components/common/plug-empty-items/plug-empty-items.component';
import {ReactiveFormsModule} from "@angular/forms";
import { TransportCargoViewPageComponent } from './pages/transport-cargo-view-page/transport-cargo-view-page.component';
import { ImagePreviewComponent } from './components/ui/image-preview/image-preview.component';
import { TypeTransportSignComponent } from './components/ui/type-transport-sign/type-transport-sign.component';
import { FieldViewItemComponent } from './components/common/field-view-item/field-view-item.component';
import { MeasurementPipe } from './pipes/measurement.pipe';
import {TransportFormPageComponent} from "./pages/transport-form-page/transport-form-page.component";
import { FilterWrapperComponent } from './components/common/filter-wrapper/filter-wrapper.component';
import { UploadImageComponent } from './components/ui/upload-image/upload-image.component';
import { ImageViewerComponent } from './components/ui/image-viewer/image-viewer.component';
import { TransportsCargoPageComponent } from './pages/transports/transports-cargo-page/transports-cargo-page.component';
import { TransportsSpecialPageComponent } from './pages/transports/transports-special-page/transports-special-page.component';
import { TransportsPassengerPageComponent } from './pages/transports/transports-passenger-page/transports-passenger-page.component';
import { TransportsViewComponent } from './components/common/transports-view/transports-view.component';
import { TransportSpecialViewPageComponent } from './pages/transport-special-view-page/transport-special-view-page.component';
import { TransportPassengerViewPageComponent } from './pages/transport-passenger-view-page/transport-passenger-view-page.component';
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';

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
    TransportFormPageComponent,
    PreloaderComponent,
    GridItemComponent,
    GridItemsComponent,
    ContactCardComponent,
    PhonePipe,
    PaginationComponent,
    PlugEmptyItemsComponent,
    TransportCargoViewPageComponent,
    ImagePreviewComponent,
    TypeTransportSignComponent,
    FieldViewItemComponent,
    MeasurementPipe,
    FilterWrapperComponent,
    UploadImageComponent,
    ImageViewerComponent,
    TransportsCargoPageComponent,
    TransportsSpecialPageComponent,
    TransportsPassengerPageComponent,
    TransportsViewComponent,
    TransportSpecialViewPageComponent,
    TransportPassengerViewPageComponent,
    ProfilePageComponent,
  ],
    imports: [
        BrowserModule, HttpClientModule, AppRoutingModule, NgOptimizedImage, ReactiveFormsModule,
    ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: RedirectInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

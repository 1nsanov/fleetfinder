import {NgModule} from '@angular/core'
import {RouterModule, Routes} from '@angular/router'
import {LandingPageComponent} from "./pages/landing-page/landing-page.component";
import {TransportsPageComponent} from "./pages/transports-page/transports-page.component";
import {AboutPageComponent} from "./pages/about-page/about-page.component";
import {namesRoute} from "./data/names-route";
import {SignUpPageComponent} from "./pages/sign-up-page/sign-up-page.component";
import {SignInPageComponent} from "./pages/sign-in-page/sign-in-page.component";
import {TransportCargoViewPageComponent} from "./pages/transport-cargo-view-page/transport-cargo-view-page.component";
import {AuthGuard} from "./guards/auth.guard";
import {TransportFormPageComponent} from "./pages/transport-form-page/transport-form-page.component";
import {TransportsCargoPageComponent} from "./pages/transports/transports-cargo-page/transports-cargo-page.component";
import {
  TransportsPassengerPageComponent
} from "./pages/transports/transports-passenger-page/transports-passenger-page.component";
import {
  TransportsSpecialPageComponent
} from "./pages/transports/transports-special-page/transports-special-page.component";
import {
  TransportSpecialViewPageComponent
} from "./pages/transport-special-view-page/transport-special-view-page.component";
import {
  TransportPassengerViewPageComponent
} from "./pages/transport-passenger-view-page/transport-passenger-view-page.component";
import {ProfilePageComponent} from "./pages/profile-page/profile-page.component";

const routes: Routes = [
  { path: namesRoute.HOME, component:  LandingPageComponent},
  { path: namesRoute.TRANSPORTS, component: TransportsPageComponent,
    children: [
      { path: namesRoute.TRANSPORTS_CARGO, component: TransportsCargoPageComponent },
      { path: namesRoute.TRANSPORTS_PASSENGER, component: TransportsPassengerPageComponent },
      { path: namesRoute.TRANSPORTS_SPECIAL, component: TransportsSpecialPageComponent },
    ]
  },
  { path: namesRoute.TRANSPORTS_MY, component: TransportsPageComponent, canActivate: [AuthGuard],
    children: [
      { path: namesRoute.TRANSPORTS_CARGO, component: TransportsCargoPageComponent },
      { path: namesRoute.TRANSPORTS_PASSENGER, component: TransportsPassengerPageComponent },
      { path: namesRoute.TRANSPORTS_SPECIAL, component: TransportsSpecialPageComponent },
    ]
  },
  { path: namesRoute.ABOUT, component: AboutPageComponent },
  { path: namesRoute.SIGN_UP, component: SignUpPageComponent },
  { path: namesRoute.SIGN_IN, component: SignInPageComponent },
  { path: namesRoute.TRANSPORT_ADD, component: TransportFormPageComponent, canActivate: [AuthGuard] },
  { path: `${namesRoute.TRANSPORT_EDIT}/:type/:id`, component: TransportFormPageComponent, canActivate: [AuthGuard] },
  { path: `${namesRoute.TRANSPORT_CARGO_VIEW}/:id`, component: TransportCargoViewPageComponent },
  { path: `${namesRoute.TRANSPORT_PASSENGER_VIEW}/:id`, component: TransportPassengerViewPageComponent },
  { path: `${namesRoute.TRANSPORT_SPECIAL_VIEW}/:id`, component: TransportSpecialViewPageComponent },
  { path: namesRoute.PROFILE, component: ProfilePageComponent, canActivate: [AuthGuard] }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

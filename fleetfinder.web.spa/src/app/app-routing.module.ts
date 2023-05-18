import {NgModule} from '@angular/core'
import {RouterModule, Routes} from '@angular/router'
import {LandingPageComponent} from "./pages/landing-page/landing-page.component";
import {TransportsPageComponent} from "./pages/transports-page/transports-page.component";
import {OrdersPageComponent} from "./pages/orders-page/orders-page.component";
import {AboutPageComponent} from "./pages/about-page/about-page.component";
import {namesRoute} from "./data/names-route";
import {SignUpPageComponent} from "./pages/sign-up-page/sign-up-page.component";
import {SignInPageComponent} from "./pages/sign-in-page/sign-in-page.component";
import {TransportCargoViewPageComponent} from "./pages/transport-cargo-view-page/transport-cargo-view-page.component";
import {AuthGuard} from "./guards/auth.guard";
import {TransportFormPageComponent} from "./pages/transport-form-page/transport-form-page.component";

const routes: Routes = [
  { path: namesRoute.HOME, component:  LandingPageComponent},
  { path: namesRoute.TRANSPORTS, component: TransportsPageComponent },
  { path: namesRoute.TRANSPORTS_MY, component: TransportsPageComponent, canActivate: [AuthGuard] },
  { path: namesRoute.ORDERS, component: OrdersPageComponent },
  { path: namesRoute.ABOUT, component: AboutPageComponent },
  { path: namesRoute.SIGN_UP, component: SignUpPageComponent },
  { path: namesRoute.SIGN_IN, component: SignInPageComponent },
  { path: namesRoute.TRANSPORT_ADD, component: TransportFormPageComponent, canActivate: [AuthGuard] },
  { path: `${namesRoute.TRANSPORT_EDIT}/:id`, component: TransportFormPageComponent, canActivate: [AuthGuard] },
  { path: `${namesRoute.TRANSPORT_CARGO_VIEW}/:id`, component: TransportCargoViewPageComponent }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

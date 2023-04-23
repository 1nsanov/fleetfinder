import {NgModule} from '@angular/core'
import {RouterModule, Routes} from '@angular/router'
import {LandingPageComponent} from "./pages/landing-page/landing-page.component";
import {TransportsPageComponent} from "./pages/transports-page/transports-page.component";
import {OrdersPageComponent} from "./pages/orders-page/orders-page.component";
import {AboutPageComponent} from "./pages/about-page/about-page.component";
import {namesRoute} from "./models/names-route";
import {SignUpPageComponent} from "./pages/sign-up-page/sign-up-page.component";
import {SignInPageComponent} from "./pages/sign-in-page/sign-in-page.component";



const routes: Routes = [
  { path: namesRoute.home, component:  LandingPageComponent},
  { path: namesRoute.transports, component: TransportsPageComponent },
  { path: namesRoute.orders, component: OrdersPageComponent },
  { path: namesRoute.about, component: AboutPageComponent },
  { path: namesRoute.signUp, component: SignUpPageComponent },
  { path: namesRoute.signIn, component: SignInPageComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

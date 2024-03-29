import { Injectable } from '@angular/core';
import {IClaims, ISignInRequest, ISignUpRequest, ITokenResponse} from "./identify.api.models";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {CookieService} from "ngx-cookie-service";
import {catchError, tap, throwError} from "rxjs";
import {TokenModel} from "../../models/token.model";
import {namesRoute} from "../../data/names-route";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class IdentifyApiService {
  url : string = environment.apiUrl + "identify/"
  claims: IClaims | null = null;

  constructor(private http: HttpClient,
              private cookieService: CookieService,
              private router: Router) { }

  signUp(request: ISignUpRequest) {
    return this.http.post<ITokenResponse>(this.url + "sign-up", request).pipe(
      tap((result) => {
        localStorage.clear();
        this.writeToken(result.Token)
      })
    );
  }
  signIn(request: ISignInRequest) {
    return this.http.post<ITokenResponse>(this.url + "sign-in", request).pipe(
      tap((result) => {
        localStorage.clear();
        this.writeToken(result.Token)
      })
    );
  }

  refreshToken(){
    const refreshToken = this.getRefreshToken();
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${this.getAccessToken()}`)
      .set('refreshToken', refreshToken);
    return this.http.post<ITokenResponse>(this.url + "refresh-token", {}, { headers })
      .pipe(
        tap((result) => {
          this.writeToken(result.Token)
        })
      );
  }

  logout() {
    localStorage.clear();
    return this.http.get<boolean>(this.url + "logout").pipe(
      tap(() => {
        this.writeToken(null)
        this.router.navigate([`/${namesRoute.HOME}`]).then(() => window.location.reload())
      }),
      catchError(error => {
        this.writeToken(null)
        this.router.navigate([`/${namesRoute.HOME}`]).then(() => window.location.reload())
        return throwError(error)
      })
    );
  }

  getClaims() {
    return this.http.get<IClaims>(this.url + "claims").pipe(
      tap((result) => {
        this.claims = result;
      })
    )
  }

  getAccessToken() : string {
    return this.cookieService.get('access_token');
  }

  isAuthenticated() : boolean {
    return !!this.getAccessToken()
  }

  getTokenExpiration() {
    const expiryTime = this.cookieService.get('expiry_time');
    return expiryTime ? Date.parse(expiryTime) : null
  }

  writeToken(token: TokenModel | null){
    this.cookieService.set('access_token', token ? token.Access : '', { path: '/'});
    this.cookieService.set('refresh_token', token ? token.Refresh : '', { path: '/'});
    this.cookieService.set('expiry_time', token ? token.ExpiryTime.toString() : '', { path: '/'})
  }

  private getRefreshToken() : string {
    return this.cookieService.get('refresh_token');
  }
}

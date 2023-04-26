import { Injectable } from '@angular/core';
import {IClaims, ISignInRequest, ISignUpRequest, ITokenResponse} from "./identify.api.models";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {CookieService} from "ngx-cookie-service";
import {catchError, tap, throwError} from "rxjs";
import {TokenModel} from "../../models/token.model";

@Injectable({
  providedIn: 'root'
})
export class IdentifyApiService {
  url : string = environment.apiUrl + "identify/"
  claims: IClaims | null = null;
  constructor(private http: HttpClient, private cookieService: CookieService) { }

  signUp(request: ISignUpRequest) {
    return  this.http.post<ITokenResponse>(this.url + "signUp", request).pipe(
      tap((result) => {
        this.writeToken(result.Token)
      })
    );
  }
  signIn(request: ISignInRequest) {
    return this.http.post<ITokenResponse>(this.url + "signIn", request).pipe(
      tap((result) => {
        this.writeToken(result.Token)
      })
    );
  }

  refreshToken(){
    const refreshToken = this.getRefreshToken();
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${this.getAccessToken()}`)
      .set('refreshToken', refreshToken);
    return this.http.post<ITokenResponse>(this.url + "refreshToken", {}, { headers })
      .pipe(
        tap((result) => {
          this.writeToken(result.Token)
        })
      );
  }

  logout() {
    return  this.http.post<boolean>(this.url + "logout", {}).pipe(
      tap((result) => {
        if (result) this.writeToken(null)
      }),
      catchError(error => {
        this.writeToken(null)
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

  testAuth() {
    const headers = new HttpHeaders()
      .set('Content-Type', 'text/plain; charset=utf-8')
    return this.http.get<string>(this.url + "test/auth", {headers});
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

  private writeToken(token: TokenModel | null){
    if (!token) {
      this.cookieService.set('access_token', '');
      this.cookieService.set('refresh_token', '');
      this.cookieService.set('expiry_time', '');
    }
    else{
      this.cookieService.set('access_token', token.Access);
      this.cookieService.set('refresh_token', token.Refresh);
      this.cookieService.set('expiry_time', token.ExpiryTime.toString())
    }
  }

  private getRefreshToken() : string {
    return this.cookieService.get('refresh_token');
  }
}

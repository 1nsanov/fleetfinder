import { Injectable } from '@angular/core';
import {ISignInRequest, ISignUpRequest, ITokenResponse} from "./identify.api.models";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class IdentifyApiService {
  constructor(private http: HttpClient) { }

  signUp(request: ISignUpRequest) {
    this.http.post<ITokenResponse>(environment.apiUrl + "signUp", request).subscribe(result => {
      //Write to куки
    }, error => console.error(error))
  }

  signIn(request: ISignInRequest) {
    this.http.post<ITokenResponse>(environment.apiUrl + "signIn", request).subscribe(result => {
      //Write to куки
    }, error => console.error(error))
  }

  refreshToken(){
    this.http.post<ITokenResponse>(environment.apiUrl + "refreshToken", {}).subscribe(result => {
      //Write to куки
    }, error => console.error(error))
  }

  logout() {
    this.http.post<boolean>(environment.apiUrl + "logout", {}).subscribe(result => {
      //Write to куки
    }, error => console.error(error))
  }

  testAuth() {
    this.http.post<string>(environment.apiUrl + "test/auth", {}).subscribe(result => {
      console.log(result)
    }, error => console.error(error))
  }
}

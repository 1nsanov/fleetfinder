import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {UserProfileGetResponse} from "./get.model";
import {UserProfilePutRequest} from "./put.model";
import {ResponseSuccessModel} from "../Common/ResponseSuccessModel";
import {UserProfilePutPassword} from "./put-password.model";

@Injectable({
  providedIn: 'root'
})
export class UserProfileApiService {
  url : string = environment.apiUrl + "user-profile"

  constructor(private http: HttpClient) {
  }

  public put(request: UserProfilePutRequest) : Observable<ResponseSuccessModel> {
    return this.http.put<ResponseSuccessModel>(this.url, request);
  }

  public changePassword(request: UserProfilePutPassword) : Observable<ResponseSuccessModel>  {
    return this.http.put<ResponseSuccessModel>(this.url + "/password", request);
  }

  public get() : Observable<UserProfileGetResponse> {
    return this.http.get<UserProfileGetResponse>(this.url);
  }
}

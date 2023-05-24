import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {ResponseIdModel} from "../Common/ResponseIdModel";
import {ResponseSuccessModel} from "../Common/ResponseSuccessModel";
import {PassengerTransportPostRequestDto} from "./post.models";
import {PassengerTransportPutRequestDto} from "./put.model";
import {PassengerTransportGetResponse} from "./get.models";
import {PassengerTransportGetListRequestDto, PassengerTransportGetListResponseDto} from "./get-list.models";

@Injectable({
  providedIn: 'root'
})
export class PassengerTransportApiService {
  url : string = environment.apiUrl + "transport/passenger"

  constructor(private http: HttpClient) {
  }

  public post(request: PassengerTransportPostRequestDto) : Observable<ResponseIdModel> {
    return this.http.post<ResponseIdModel>(this.url, request);
  }

  public put(request: PassengerTransportPutRequestDto) : Observable<ResponseIdModel>  {
    return this.http.put<ResponseIdModel>(this.url, request);
  }

  public get(id: number)  : Observable<PassengerTransportGetResponse> {
    const params = new HttpParams()
      .set('id', id.toString());
    return this.http.get<PassengerTransportGetResponse>(this.url, { params: params });
  }

  public getList(request : PassengerTransportGetListRequestDto) : Observable<PassengerTransportGetListResponseDto>  {
    const params = new HttpParams()
      .set('pageSize', request.pageSize)
      .set('skipCount', request.skipCount)
      .set('sortParameter', request.sortParameter)
      .set('sortDesc', request.sortDesc)
      .set('UserFilter', request.filter.UserFilter ?? "")
      .set('TitleFilter', request.filter.TitleFilter ?? "")
      .set('RegionFilter', request.filter.RegionFilter ?? "")
      .set('TypeFilter', request.filter.TypeFilter ?? "");
    return this.http.get<PassengerTransportGetListResponseDto>(this.url + "/list", {params: params});
  }

  public delete(id : number) : Observable<ResponseSuccessModel> {
    const params = new HttpParams()
      .set('id', id.toString());
    return this.http.delete<ResponseSuccessModel>(this.url, { params: params });
  }
}

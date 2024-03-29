import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {ResponseIdModel} from "../Common/ResponseIdModel";
import {SpecialTransportGetListResponseDto, SpecialTransportGetListRequestDto} from "./get-list.models";
import {SpecialTransportGetResponse} from "./get.models";
import {ResponseSuccessModel} from "../Common/ResponseSuccessModel";
import {SpecialTransportPostRequestDto} from "./post.models";
import {SpecialTransportPutRequestDto} from "./put.model";

@Injectable({
  providedIn: 'root'
})
export class SpecialTransportApiService {
  url : string = environment.apiUrl + "transport/special"

  constructor(private http: HttpClient) {
  }

  public post(request: SpecialTransportPostRequestDto) : Observable<ResponseIdModel> {
    return this.http.post<ResponseIdModel>(this.url, request);
  }

  public put(request: SpecialTransportPutRequestDto) : Observable<ResponseIdModel>  {
    return this.http.put<ResponseIdModel>(this.url, request);
  }

  public get(id: number)  : Observable<SpecialTransportGetResponse> {
    const params = new HttpParams()
      .set('id', id.toString());
    return this.http.get<SpecialTransportGetResponse>(this.url, { params: params });
  }

  public getList(request : SpecialTransportGetListRequestDto) : Observable<SpecialTransportGetListResponseDto>  {
    const params = new HttpParams()
      .set('pageSize', request.pageSize)
      .set('skipCount', request.skipCount)
      .set('sortParameter', request.sortParameter)
      .set('sortDesc', request.sortDesc)
      .set('UserFilter', request.filter.UserFilter ?? "")
      .set('TitleFilter', request.filter.TitleFilter ?? "")
      .set('RegionFilter', request.filter.RegionFilter ?? "")
      .set('TypeFilter', request.filter.TypeFilter ?? "");
    return this.http.get<SpecialTransportGetListResponseDto>(this.url + "/list", {params: params});
  }

  public delete(id : number) : Observable<ResponseSuccessModel> {
    const params = new HttpParams()
      .set('id', id.toString());
    return this.http.delete<ResponseSuccessModel>(this.url, { params: params });
  }
}

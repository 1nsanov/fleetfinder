import {Injectable, Query} from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {ResponseIdModel} from "../Common/ResponseIdModel";
import {CargoTransportPostRequestDto} from "./post.models";
import {CargoTransportGetListRequestDto, CargoTransportGetListResponseDto} from "./get-list.models";
import {CargoTransportGetResponse} from "./get.models";
import {CargoTransportPutRequestDto} from "./put.model";
import {Observable} from "rxjs";
import {ResponseSuccessModel} from "../Common/ResponseSuccessModel";

@Injectable({
  providedIn: 'root'
})
export class CargoTransportApiService {
  url : string = environment.apiUrl + "cargo/transport"

  constructor(private http: HttpClient) {
  }

  public post(request: CargoTransportPostRequestDto) : Observable<ResponseIdModel> {
    return this.http.post<ResponseIdModel>(this.url, request);
  }

  public put(request: CargoTransportPutRequestDto) : Observable<ResponseIdModel>  {
    return this.http.put<ResponseIdModel>(this.url, request);
  }

  public get(id: number)  : Observable<CargoTransportGetResponse> {
    const params = new HttpParams()
      .set('id', id.toString());
    return this.http.get<CargoTransportGetResponse>(this.url, { params: params });
  }

  public getList(request : CargoTransportGetListRequestDto)  : Observable<CargoTransportGetListResponseDto>  {
    const params = new HttpParams()
      .set('pageSize', request.pageSize)
      .set('skipCount', request.skipCount)
      .set('sortParameter', request.sortParameter)
      .set('sortDesc', request.sortDesc)
      .set('UserFilter', request.filter.UserFilter ?? "")
      .set('TitleFilter', request.filter.TitleFilter ?? "")
      .set('RegionFilter', request.filter.RegionFilter ?? "")
      .set('TypeFilter', request.filter.TypeFilter ?? "");
    return this.http.get<CargoTransportGetListResponseDto>(this.url + "/list", {params: params});
  }

  public delete(id : number) : Observable<ResponseSuccessModel> {
    const params = new HttpParams()
      .set('id', id.toString());
    return this.http.delete<ResponseSuccessModel>(this.url, { params: params });
  }
}

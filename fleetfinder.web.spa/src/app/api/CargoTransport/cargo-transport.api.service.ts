import {Injectable, Query} from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {IResponseId} from "../Common/IResponseId";
import {CargoTransportPostRequestDto} from "./post.models";
import {CargoTransportGetListRequestDto, CargoTransportGetListResponseDto} from "./get-list.models";
import {CargoTransportGetResponse} from "./get.models";

@Injectable({
  providedIn: 'root'
})
export class CargoTransportApiService {
  url : string = environment.apiUrl + "cargo/transport"

  constructor(private http: HttpClient) {
  }

  post(request: CargoTransportPostRequestDto) {
    return this.http.post<IResponseId>(this.url, request);
  }

  getList(request : CargoTransportGetListRequestDto) {
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

  get(id: number){
    const params = new HttpParams()
      .set('id', id.toString());
    return this.http.get<CargoTransportGetResponse>(this.url, { params: params });
  }
}

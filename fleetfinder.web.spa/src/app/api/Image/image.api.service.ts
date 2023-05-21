import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {ImagePostRequest} from "./post.models";
import {Observable} from "rxjs";
import {ImageDeleteRequest} from "./delete.models";
import {IdentifyApiService} from "../Identify/identify.api.service";

@Injectable({
  providedIn: 'root'
})
export class ImageApiService {
  url : string = environment.apiUrl + "image"
  constructor(private http: HttpClient,
              private identifyService: IdentifyApiService) { }

  async upload(request: ImagePostRequest) : Promise<string[]> {
    if (request.Files.length === 0) return [];
    let formDataArray : FormData[] = [];
    for (let i = 0; i < request.Files.length; i++) {
        const formData = new FormData();
        formData.append('file', request.Files[i]);
        formDataArray.push(formData);
    }

    const formDataFolder = new FormData();
      formDataFolder.append('folder', request.Folder);
      formDataArray.push(formDataFolder);

    const parentFormData = new FormData();
    formDataArray.forEach((formData) => {
      parentFormData.append(`Files`, formData.get('file') as Blob);
    });
    parentFormData.append('Folder', request.Folder)


    const response = await fetch(this.url, {
      method: 'POST',
      body: parentFormData,
      headers: {
        'Authorization': `Bearer ${this.identifyService.getAccessToken()}`,
        'UserId': this.identifyService.claims?.Id?.toString() ?? ''
      },
    });

    if (response.ok) {
      const data = await response.json();
      return data as string[];
    } else {
      console.error('Request failed:', response.status);
      return [];
    }
  }

  delete(request: ImageDeleteRequest) : Observable<Object> {
    let query = `?folder=${request.Folder}`;
    request.Urls.forEach(url => {
      query += `&url=${url}`;
    });
    return this.http.delete(this.url + query);
  }
}

import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import {environment} from "../environments/environment";
import {GlobalLoaderService} from "./services/global-loader.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  baseUrl: string = environment.apiUrl;
  constructor(http: HttpClient,
              public loaderService: GlobalLoaderService) {
    http.get<any>(this.baseUrl + 'weatherforecast').subscribe(result => {
      console.log("Success connected to server! Test Data: ", result)
      setTimeout(() => loaderService.stop(), 200);
    }, error => console.error(error));
  }
  title = 'FLEETFINDER';
}

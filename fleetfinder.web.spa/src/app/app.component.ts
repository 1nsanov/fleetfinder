import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import {environment} from "../environments/environment";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  baseUrl: string = environment.apiUrl;
  constructor(http: HttpClient) {
    http.get<any>(this.baseUrl + 'weatherforecast').subscribe(result => {
      console.log("Success connected to server! Test Data: ", result)
    }, error => console.error(error));
  }
  title = 'FLEETFINDER';
}

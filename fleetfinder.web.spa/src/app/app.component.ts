import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(http: HttpClient) {
    http.get<any>('/weatherforecast').subscribe(result => {
      console.log("Success connected to server! Test Data: ", result)
    }, error => console.error(error));
  }
  title = 'FLEETFINDER';
}

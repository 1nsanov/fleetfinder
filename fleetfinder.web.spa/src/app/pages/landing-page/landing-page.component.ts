import { Component } from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent {
  constructor(private identifyService: IdentifyApiService) {
  }
  checkAuth(){
    this.identifyService.testAuth().subscribe(() => {
      console.log("yes")
    })
  }
}

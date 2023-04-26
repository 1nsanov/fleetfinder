import { Component } from '@angular/core';
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {NotificationService} from "../../services/notification.service";

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent {
  isLoad = false;
  constructor(private identifyService: IdentifyApiService,
              private notification: NotificationService) {
  }
  checkAuth(){
    this.isLoad = true;
    this.identifyService.testAuth().subscribe(
      () => {
        this.isLoad = false;
        this.notification.notify("Вы авторизированы!")
      },
      error => {
        this.isLoad = false;
        this.notification.notify("Вы не авторизовались!")
      }
    )
  }
}

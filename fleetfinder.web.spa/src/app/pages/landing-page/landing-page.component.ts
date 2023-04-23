import { Component } from '@angular/core';
import {ModalService} from "../../services/modal.service";

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent {
  constructor(public modalService: ModalService) {
  }
  valueTest: string = ""
  valueTest2: string = ""
  clickButton(){
    console.log("Click!")
  }
}

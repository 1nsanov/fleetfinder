import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-preloader',
  templateUrl: './preloader.component.html',
  styleUrls: ['./preloader.component.scss']
})
export class PreloaderComponent {
  @Input() load: boolean = false;
  @Input() text: string = "";
  @Input() height: string = "300px";

  innerLoad = false;


  get isHidden(){
    setTimeout(() => {
      this.innerLoad = this.load;
    }, 300)
    return !this.load;
  }
}

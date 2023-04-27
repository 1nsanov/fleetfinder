import {Component, OnInit} from '@angular/core';
import {NotificationService} from "../../../services/notification.service";
import {NotifyModel} from "../../../models/notify.model";
import {NotificationTheme} from "../../../models/enums/notification-theme.enum";

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit{
  notify: NotifyModel | null = null;
  isShow = false;
  isAnimationFadeOut : boolean = false;
  _theme = NotificationTheme;
  isAnimationLine = false;
  setTimeoutIsAnimationFadeOut: any;
  setTimeoutHidden: any;

  constructor(public notification: NotificationService) {}

  ngOnInit(): void {
    this.notification.notify$.subscribe((notify) => {
      if (!notify.text) return;
      if (this.isShow) this.hidden();

      this.notify = notify;
      this.isShow = true;
      setTimeout(() => this.isAnimationLine = true, 10);
      this.setTimeoutIsAnimationFadeOut = setTimeout(() => this.isAnimationFadeOut = true, 4400)
      this.setTimeoutHidden = setTimeout(() => {
        this.hidden();
      }, 4700)
    })
  }

  hidden(){
    this.isShow = false;
    this.isAnimationFadeOut = false;
    this.notify = null;
    this.isAnimationLine = false;
    clearTimeout(this.setTimeoutIsAnimationFadeOut);
    clearTimeout(this.setTimeoutHidden);
  }
}

import {NotificationTheme} from "./enums/notification-theme.enum";

export class NotifyModel {
  title: string = "";
  text: string = "";
  theme: NotificationTheme = NotificationTheme.Info;
  constructor(title: string, text: string, theme: NotificationTheme) {
    this.title = title;
    this.text = text;
    this.theme = theme;
  }
}

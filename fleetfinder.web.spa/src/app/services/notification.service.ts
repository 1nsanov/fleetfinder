import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {NotificationTheme} from "../models/enums/notification-theme.enum";
import {NotifyModel} from "../models/notify.model";



@Injectable({
  providedIn: 'root'
})
export class NotificationService{

  public notify$ = new BehaviorSubject<NotifyModel>(new NotifyModel("", "", NotificationTheme.Info));

  notify(text: string, title: string = "Оповещение") {
    this.notify$.next(new NotifyModel(title, text, NotificationTheme.Info))
  }

  error(text: string, title: string = "Ошибка") {
    this.notify$.next(new NotifyModel(title, text, NotificationTheme.Error))
  }
}

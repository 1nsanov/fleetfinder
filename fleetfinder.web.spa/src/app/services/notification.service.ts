import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {NotificationTheme} from "../models/enums/notification-theme.enum";
import {NotifyModel} from "../models/notify.model";
import {HttpErrorResponse} from "@angular/common/http";



@Injectable({
  providedIn: 'root'
})
export class NotificationService{

  public notify$ = new BehaviorSubject<NotifyModel>(new NotifyModel("", "", NotificationTheme.Info));

  public notify(text: string, title: string = "Оповещение") {
    this.notify$.next(new NotifyModel(title, text, NotificationTheme.Info))
  }

  public error(text: string, title: string = "Ошибка") {
    this.notify$.next(new NotifyModel(title, text, NotificationTheme.Error))
  }

  public errorFromHttp(error: HttpErrorResponse) {
    const errorMessages = error.error.errors;
    const errorArray = Object.values(errorMessages).flat();
    const errorString = errorArray.join('<br>');
    this.error(errorString);
  }
}

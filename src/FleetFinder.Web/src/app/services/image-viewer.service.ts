import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ImageViewerService {
  isOpened$ = new BehaviorSubject<boolean>(false)
  open() {
    this.isOpened$.next(true)
  }
  close() {
    this.isOpened$.next(false)
  }
}

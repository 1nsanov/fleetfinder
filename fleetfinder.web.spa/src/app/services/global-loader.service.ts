import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class GlobalLoaderService {
  loading$ = new BehaviorSubject<boolean>(true)
  start() {
    this.loading$.next(true)
  }
  stop() {
    this.loading$.next(false)
  }
}

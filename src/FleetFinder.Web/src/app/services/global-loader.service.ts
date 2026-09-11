import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class GlobalLoaderService {
  loading$ = new BehaviorSubject<boolean>(true)
  title$ = new BehaviorSubject<string>("Загрузка данных...")

  constructor() {
    document.body.style.overflow = 'hidden';
  }

  changeTitle(title: string){
    setTimeout(() => this.title$.next(title), 250);
    setTimeout(() => this.stop(), 1100)
  }
  stop() {
    this.loading$.next(false)
    document.body.style.overflow = 'auto';
  }
}

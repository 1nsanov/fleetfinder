import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TimeoutService {

  constructor() { }

  async wait(ms : number): Promise<void> {
    return new Promise<void>((resolve) => {
      setTimeout(() => {
        resolve();
      }, ms);
    });
  }
}

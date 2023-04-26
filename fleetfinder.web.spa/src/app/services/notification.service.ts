import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  notify(text: string) {
    alert(text)
  }

  error(text: string) {
    alert("Error: " + text)
  }
}

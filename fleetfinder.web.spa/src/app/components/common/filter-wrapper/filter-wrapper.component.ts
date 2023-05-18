import {Component, HostListener, Input} from '@angular/core';

@Component({
  selector: 'app-filter-wrapper',
  templateUrl: './filter-wrapper.component.html',
  styleUrls: ['./filter-wrapper.component.scss']
})
export class FilterWrapperComponent {
  @Input() count: number = 0;

  isOpenPopup = false;

  openPopup() {
    this.isOpenPopup = !this.isOpenPopup;
  }
  closePopup() {
    this.isOpenPopup = false;
  }

  @HostListener('document:click', ['$event.target'])
  onClick(target: any) {
    if (target.classList.contains('i-c-p')) return;
    const isInsidePopup = target.closest('.popup-container');
    if (!isInsidePopup) {
      this.closePopup();
    }
  }
}

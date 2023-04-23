import {Component, EventEmitter, HostListener, Input, OnInit, Output} from '@angular/core';
import {ModalService} from "../../../services/modal.service";

@Component({
  selector: 'app-modal-window',
  templateUrl: './modal-window.component.html',
  styleUrls: ['./modal-window.component.scss']
})
export class ModalWindowComponent implements OnInit{
  @Input() title: string;
  @Input() width: string = '400px';
  @Output() closed = new EventEmitter<void>();
  isVisible: boolean = false;

  constructor(private modalService: ModalService) {}

  ngOnInit(): void {
    this.modalService.isVisible$.subscribe(isVisible => {
      this.isVisible = isVisible;
    })
  }

  closeModal() {
    this.isVisible = false;
    setTimeout(() => {
      this.modalService.close()
      this.closed.emit();
    }, 290);
  }

  @HostListener('document:keydown.escape', ['$event']) onEscapeKeydown(event: KeyboardEvent) {
    this.closeModal();
  }
}

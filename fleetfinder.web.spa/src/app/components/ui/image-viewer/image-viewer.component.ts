import {Component, EventEmitter, HostListener, Input, Output} from '@angular/core';
import {TimeoutService} from "../../../services/timeout.service";
import {ImageViewerService} from "../../../services/image-viewer.service";

@Component({
  selector: 'app-image-viewer',
  templateUrl: './image-viewer.component.html',
  styleUrls: ['./image-viewer.component.scss']
})
export class ImageViewerComponent {
  @Input() image: string;

  @Output() closed = new EventEmitter<void>();
  isOpened: boolean = false;

  constructor(private imageViewerService: ImageViewerService,
              private timeoutService: TimeoutService) {}

  ngOnInit(): void {
    this.imageViewerService.isOpened$.subscribe(isOpened => {
      this.isOpened = isOpened;
    })
  }

  async closeModal() {
    this.isOpened = false;
    await this.timeoutService.wait(300)
    this.imageViewerService.close()
    this.closed.emit();
  }

  @HostListener('document:keydown.escape', ['$event']) async onEscapeKeydown(event: KeyboardEvent) {
    await this.closeModal();
  }
}

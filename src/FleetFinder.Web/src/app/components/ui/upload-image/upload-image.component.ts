import {Component, ElementRef, EventEmitter, Input, Output, ViewChild, ChangeDetectionStrategy} from '@angular/core';
import {NotificationService} from "../../../services/notification.service";

@Component({
    selector: 'app-upload-image',
    templateUrl: './upload-image.component.html',
    styleUrls: ['./upload-image.component.scss'],
    changeDetection: ChangeDetectionStrategy.Eager,
    standalone: false
})
export class UploadImageComponent {
  @Input() disabled: boolean = false;

  @Output() onUpload = new EventEmitter<File | null>();

  @ViewChild('inputFile')
  inputFile: ElementRef;

  constructor(private notification: NotificationService) {
  }

  onFileSelected(event: any) {
    let file = event.target.files[0] as File | undefined;
    if (!file) return;

    const maxSizeInBytes = 10 * 1024 * 1024;
    const allowedExtensions = ['png', 'jpg', 'jpeg'];
    const allowedMimeTypes = ['image/png', 'image/jpeg', 'image/jpg'];
    const fileExtension = this.getFileExtension(file.name);
    const mimeOk = !file.type || allowedMimeTypes.includes(file.type.toLowerCase());

    if (!allowedExtensions.includes(fileExtension) || !mimeOk) {
      file = undefined;
      this.notification.error('Неверный формат файла. Разрешены только файлы PNG, JPG и JPEG.');
    } else if (file.size > maxSizeInBytes) {
      file = undefined;
      this.notification.error('Максимальный размер файла - 10 Мб.');
    }

    this.onUpload.emit(file ?? null);
    this.inputFile.nativeElement.value = "";
  }

  private getFileExtension(fileName: string): string {
    const dotIndex = fileName.lastIndexOf('.');
    if (dotIndex === -1)
      return '';
    return fileName.substr(dotIndex + 1).toLowerCase();
  }
}

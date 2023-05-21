import {Component, ElementRef, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {NotificationService} from "../../../services/notification.service";

@Component({
  selector: 'app-upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.scss']
})
export class UploadImageComponent {
  @Input() disabled: boolean = false;

  @Output() onUpload = new EventEmitter<File | null>();

  @ViewChild('inputFile')
  inputFile: ElementRef;

  constructor(private notification: NotificationService) {
  }

  onFileSelected(event: any) {
    let file = event.target.files[0];
    const maxSizeInBytes = 10 * 1024 * 1024; // 10 MB
    const fileExtension = this.getFileExtension(file.name);

    if (fileExtension !== 'png' && fileExtension !== 'jpg' && fileExtension !== 'jpeg'){
      file = null;
      this.notification.error('Неверный формат файла. Разрешены только файлы PNG, JPG и JPEG.');
    }
    else if (file && file.size > maxSizeInBytes) {
      file = null;
      this.notification.error('Максимальный размер файла - 10 Мб.')
    }

    this.onUpload.emit(file);
    this.inputFile.nativeElement.value = "";
  }

  private getFileExtension(fileName: string): string {
    const dotIndex = fileName.lastIndexOf('.');
    if (dotIndex === -1)
      return '';
    return fileName.substr(dotIndex + 1).toLowerCase();
  }
}

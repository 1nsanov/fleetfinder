import {Component, EventEmitter, Input, OnChanges, Output} from '@angular/core';

@Component({
  selector: 'app-image-preview',
  templateUrl: './image-preview.component.html',
  styleUrls: ['./image-preview.component.scss']
})
export class ImagePreviewComponent implements OnChanges{
  @Input() previewFile: File | null = null;
  @Input() images: string[] = [];
  @Input() mode: string = "view"

  @Output() confirm = new EventEmitter<File | null>();
  @Output() cancel = new EventEmitter<void>();
  @Output() remove = new EventEmitter<string>();

  innerPreviewImg : string | ArrayBuffer | null = null;

  ngOnChanges(changes: any): void {
    if(!changes.previewFile) return;
    const file = changes.previewFile.currentValue;
    if (file instanceof File) {
      console.log(changes.previewFile.currentValue)
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.innerPreviewImg = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }

  onConfirm() {
    this.confirm.emit(this.previewFile);
    setTimeout(() => this.clearPreview(), 50);
  }

  onCancel(){
    this.cancel.emit();
    this.clearPreview();
  }

  onRemove(url : string) {
    const target = this.images.find(x => x === url) ?? "";
    this.remove.emit(target);
  }

  clearPreview(){
    this.innerPreviewImg = null;
  }
}

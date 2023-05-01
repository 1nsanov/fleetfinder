import {Component, EventEmitter, Input, Output, OnInit} from '@angular/core';
import {DomSanitizer} from "@angular/platform-browser";
import {InputService} from "../../../services/input.service";

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss']
})
export class InputComponent implements OnInit{
  @Input() value: string
  @Input() placeholder: string = ""
  @Input() label: string;
  @Input() icon: string;
  @Input() type: string = "text";
  @Input() error: string = "";
  @Input() vDropdown: boolean = false;

  @Output() valueChange = new EventEmitter<string>();
  @Output() click = new EventEmitter<void>();
  @Output() focus = new EventEmitter<void>();
  @Output() blur = new EventEmitter<void>();

  constructor(private sanitizer: DomSanitizer) {
  }

  service: InputService;

  ngOnInit(): void {
    this.service = new InputService(this.sanitizer, this.icon);
    if (!this.value) this.value = "";
    if (!this.label)
      this.service.blockState();
    else if (this.value)
      this.service.switchState(true);
  }

  onInput(e: Event){
    this.service.switchState(true);
    this.valueChange.emit((e.target as HTMLInputElement).value)
  }
  onClick(e: Event) {
    e.stopPropagation();
    if (this.vDropdown) this.click.emit();
  }

  onFocus(){
    this.service.onFocusEvent()
    this.focus.emit();
  }

  onBlur(){
    setTimeout(() => {
      this.service.onBlurEvent(!!this.value);
      this.error = "";
      this.blur.emit()
    }, 100)
  }
}

import {
  Component,
  EventEmitter,
  Input,
  Output,
  OnInit,
  OnChanges,
  SimpleChanges,
  ElementRef,
  Renderer2
} from '@angular/core';
import {DomSanitizer} from "@angular/platform-browser";
import {InputService} from "../../../services/input.service";
import {TimeoutService} from "../../../services/timeout.service";

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss']
})
export class InputComponent implements OnInit, OnChanges {
  @Input() value: string;
  @Input() placeholder: string = ""
  @Input() label: string;
  @Input() icon: string;
  @Input() type: string = "text";
  @Input() error: string = "";
  @Input() vDropdown: boolean = false;
  @Input() disabled: boolean = false;
  @Input() mask: string;

  @Output() valueChange = new EventEmitter<any>();
  @Output() click = new EventEmitter<void>();
  @Output() focus = new EventEmitter<void>();
  @Output() blur = new EventEmitter<void>();

  service: InputService;

  constructor(private sanitizer: DomSanitizer,
              private timeoutService: TimeoutService,
              private elementRef: ElementRef,
              private renderer: Renderer2) {
  }

  ngOnInit(): void {
    this.service = new InputService(this.sanitizer, this.icon);
    if (!this.value) this.value = "";
    if (!this.label)
      this.service.blockState();
    else if (this.value)
      this.service.switchState(true);

    // if (this.mask)
    //   this.renderer.setAttribute(this.elementRef.nativeElement.querySelector(`#${this.service.id.substring(0, 8)}`), 'mask', this.mask);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.service && changes.value)
      this.service.switchState(!!this.value);
  }

  onInput(e: Event){
    this.service.switchState(true);
    this.valueChange.emit(!!(e.target as HTMLInputElement).value ? (e.target as HTMLInputElement).value : null)
  }

  onClick(e: Event) {
    e.stopPropagation();
    if (this.vDropdown) this.click.emit();
  }

  onFocus(){
    this.service.onFocusEvent()
    this.focus.emit();
  }

  async onBlur(){
    await this.timeoutService.wait(100);
    this.service.onBlurEvent(!!this.value);
    this.blur.emit()
  }
}

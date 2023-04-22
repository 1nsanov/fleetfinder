import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BtnTheme} from "../../../models/enums/btn-theme.enum";

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent {
  @Input() theme: string = "blue"
  @Input() disabled : boolean = false;

  @Output() click = new EventEmitter<void>();

  BtnTheme = BtnTheme;
  onClick(e: Event) {
    e.stopPropagation();
    if (!this.disabled) this.click.emit();
  }
}

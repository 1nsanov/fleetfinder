import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {BtnTheme} from "../../../models/enums/btn-theme.enum";

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent implements OnInit{
  @Input() theme: string = "blue"
  @Input() disabled : boolean = false;
  @Input() isLoad: boolean = false;
  @Input() size: string = "default";
  @Input() iconPath: string = "";

  @Output() click = new EventEmitter<void>();

  BtnTheme = BtnTheme;
  colorLoading = "white";
  ngOnInit(): void {
    const currentTheme = this.theme as BtnTheme;
    if (currentTheme === BtnTheme.BlueLight || currentTheme === BtnTheme.Yellow || currentTheme === BtnTheme.White)
      this.colorLoading = "black";
  }
  onClick(e: Event) {
    e.stopPropagation();
    if (!this.disabled && !this.isLoad) this.click.emit();
  }
}

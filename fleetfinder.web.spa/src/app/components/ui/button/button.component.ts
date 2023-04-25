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

  @Output() click = new EventEmitter<void>();

  BtnTheme = BtnTheme;
  currentTheme : BtnTheme = BtnTheme.Blue;

  ngOnInit(): void {
    this.currentTheme = this.theme as BtnTheme;
    console.log(this.currentTheme === BtnTheme.Blue)
  }
  onClick(e: Event) {
    e.stopPropagation();
    if (!this.disabled) this.click.emit();
  }

  get colorLoading (){
    return "white";
  }
}

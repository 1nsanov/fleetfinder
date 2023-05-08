import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {DropdownItemModel} from "../../../models/dropdown-item.model";

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss']
})
export class DropdownComponent implements OnInit{
  @Input() label: string;
  @Input() placeholder: string = ""
  @Input() items: Array<DropdownItemModel<any>> = [];
  @Input() defaultValue: DropdownItemModel<any> | null = null;

  @Output() select = new EventEmitter<DropdownItemModel<any>>();
  current: DropdownItemModel<any> | null = null;
  valuePreview: string = "";
  isOpened: boolean = false;

  ngOnInit(): void {
    if (this.defaultValue) {
      this.current = this.defaultValue;
      this.valuePreview = this.defaultValue.Preview;
    }
  }

  onFocus(){
    this.isOpened = true;
  }

  onBlur(){
    this.isOpened = false;
  }

  selectItem(item : DropdownItemModel<any>){
    this.current = item;
    this.valuePreview = item.Preview;
    this.select.emit(this.current)
  }
}

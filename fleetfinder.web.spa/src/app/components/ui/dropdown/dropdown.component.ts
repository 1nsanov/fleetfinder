import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {DropdownItemModel} from "../../../models/dropdown-item.model";

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss']
})
export class DropdownComponent{
  @Input() label: string;
  @Input() placeholder: string = ""
  @Input() items: Array<DropdownItemModel<any>> = [];
  @Input() value: DropdownItemModel<any> | null = null;
  @Input() error: string = "";

  @Output() select = new EventEmitter<DropdownItemModel<any>>();

  isOpened: boolean = false;

  onFocus(){
    this.isOpened = true;
  }

  onBlur(){
    this.isOpened = false;
  }

  selectItem(item : DropdownItemModel<any>){
    this.value = item;
    this.select.emit(this.value);
  }
}

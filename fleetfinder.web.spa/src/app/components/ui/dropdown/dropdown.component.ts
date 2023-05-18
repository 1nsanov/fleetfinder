import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {DropdownItemModel} from "../../../models/dropdown-item.model";

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss']
})
export class DropdownComponent implements OnInit, OnChanges{
  @Input() label: string;
  @Input() placeholder: string = ""
  @Input() items: Array<DropdownItemModel<any>> = [];
  @Input() value: DropdownItemModel<any> | null = null;
  @Input() error: string = "";

  @Output() select = new EventEmitter<DropdownItemModel<any>>();
  // current: DropdownItemModel<any> | null = null;
  // valuePreview: string = "";
  isOpened: boolean = false;

  ngOnInit(): void {
    // if (this.value) {
    //   this.current = this.value;
    //   this.valuePreview = this.value.Preview;
    // }
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes)
  }

  onFocus(){
    this.isOpened = true;
  }

  onBlur(){
    this.isOpened = false;
  }

  selectItem(item : DropdownItemModel<any>){
    this.value = item;
    // this.valuePreview = item.Preview;
    this.select.emit(this.value);
  }
}

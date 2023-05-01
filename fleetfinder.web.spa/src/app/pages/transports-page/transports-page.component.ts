import { Component } from '@angular/core';
import {DropdownItemModel} from "../../models/dropdown-item.model";

@Component({
  selector: 'app-transports-page',
  templateUrl: './transports-page.component.html',
  styleUrls: ['./transports-page.component.scss']
})
export class TransportsPageComponent {
  items : Array<DropdownItemModel<string>> = [
    new DropdownItemModel(1, "item1", "Item 1"),
    new DropdownItemModel(2, "item2", "Item 2"),
    new DropdownItemModel(3, "item3", "Item 3"),
    new DropdownItemModel(4, "item4", "Item 4"),
    new DropdownItemModel(5, "item5", "Item 5")
  ]

  onSelect(item : DropdownItemModel<string>){
    console.log(item);
  }
}

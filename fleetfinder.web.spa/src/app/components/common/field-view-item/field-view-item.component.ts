import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-field-view-item',
  templateUrl: './field-view-item.component.html',
  styleUrls: ['./field-view-item.component.scss']
})
export class FieldViewItemComponent {
  @Input() title: string = ""
  @Input() text: string | null;
}

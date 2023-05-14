import {Component, Input} from '@angular/core';
import {IGridItem} from "../../../models/interfaces/grid-item.interface";
import {TransportType} from "../../../models/enums/transport/transport-type.enum";

@Component({
  selector: 'app-grid-items',
  templateUrl: './grid-items.component.html',
  styleUrls: ['./grid-items.component.scss']
})
export class GridItemsComponent {
  @Input() items: IGridItem[] | null;
  @Input() type: TransportType;
}

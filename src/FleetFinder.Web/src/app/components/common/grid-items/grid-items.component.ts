import {Component, Input, ChangeDetectionStrategy} from '@angular/core';
import {IGridItem} from "../../../models/interfaces/grid-item.interface";
import {TransportType} from "../../../models/enums/transport/transport-type.enum";

@Component({
    selector: 'app-grid-items',
    templateUrl: './grid-items.component.html',
    styleUrls: ['./grid-items.component.scss'],
    changeDetection: ChangeDetectionStrategy.Eager,
    standalone: false
})
export class GridItemsComponent {
  @Input() items: IGridItem[] | null;
  @Input() type: TransportType;
}

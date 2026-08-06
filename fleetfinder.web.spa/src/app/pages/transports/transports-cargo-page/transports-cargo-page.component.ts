import { Component, ChangeDetectionStrategy } from '@angular/core';
import {TransportType} from "../../../models/enums/transport/transport-type.enum";

@Component({
    selector: 'app-transports-cargo-page',
    templateUrl: './transports-cargo-page.component.html',
    styleUrls: ['./transports-cargo-page.component.scss'],
    changeDetection: ChangeDetectionStrategy.Eager,
    standalone: false
})
export class TransportsCargoPageComponent {
  TransportType = TransportType;
}

import { Component, ChangeDetectionStrategy } from '@angular/core';
import { TransportType } from 'src/app/models/enums/transport/transport-type.enum';

@Component({
    selector: 'app-transports-special-page',
    templateUrl: './transports-special-page.component.html',
    styleUrls: ['./transports-special-page.component.scss'],
    changeDetection: ChangeDetectionStrategy.Eager,
    standalone: false
})
export class TransportsSpecialPageComponent {
  TransportType = TransportType;
}

import { Component } from '@angular/core';
import { TransportType } from 'src/app/models/enums/transport/transport-type.enum';

@Component({
  selector: 'app-transports-passenger-page',
  templateUrl: './transports-passenger-page.component.html',
  styleUrls: ['./transports-passenger-page.component.scss']
})
export class TransportsPassengerPageComponent {
  TransportType = TransportType;
}

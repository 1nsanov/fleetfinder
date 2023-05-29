import { Component } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {TransportService} from "../../services/transport.service";
import {catchError, throwError} from "rxjs";
import {HttpErrorResponse} from "@angular/common/http";
import {namesRoute} from "../../data/names-route";
import {ExperienceWorkConst, PaymentOrderConst, PaymentMethodConst, RegionConst, PassengerRentalDurationConst, PassengerFacilitiesConst, PassengerOptionConst, PassengerTransportationKindConst } from 'src/app/data/enums.data';
import { TransportType } from 'src/app/models/enums/transport/transport-type.enum';
import {PassengerTransportApiService} from "../../api/PassengerTransport/passenger-transport.api.service";
import {PassengerTransportGetResponse} from "../../api/PassengerTransport/get.models";
import {PassengerType} from "../../models/enums/transport/passenger/passenger-type.enum";
import {passengerItems} from "../../data/transport/passenger-items";
import {PassengerRentalDuration} from "../../models/enums/transport/passenger/passenger-rental-duration.enum";
import {PassengerFacilities} from "../../models/enums/transport/passenger/passenger-facilities.enum";
import {PassengerOption} from "../../models/enums/transport/passenger/passenger-option.enum";
import {PassengerTransportationKind} from "../../models/enums/transport/passenger/passenger-transportation-kind.enum";

@Component({
  selector: 'app-transport-passenger-view-page',
  templateUrl: './transport-passenger-view-page.component.html',
  styleUrls: ['./transport-passenger-view-page.component.scss']
})
export class TransportPassengerViewPageComponent {
  RegionConst = RegionConst;
  ExperienceWorkConst = ExperienceWorkConst;
  PaymentMethodConst = PaymentMethodConst;
  PaymentOrderConst = PaymentOrderConst;
  PassengerRentalDurationConst = PassengerRentalDurationConst;
  PassengerFacilitiesConst = PassengerFacilitiesConst;
  PassengerOptionConst = PassengerOptionConst;
  PassengerTransportationKindConst = PassengerTransportationKindConst;
  TransportType = TransportType;
  typeImg: string | null;
  typeHint: string = "";
  transport: PassengerTransportGetResponse | null;
  sizeText = "";
  constructor(private route: ActivatedRoute,
              private router: Router,
              private passengerTransportService: PassengerTransportApiService,
              private identifyService: IdentifyApiService,
              public transportService: TransportService) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) this.getItem(parseInt(id));
    });
  }

  getItem(id: number){
    this.passengerTransportService.get(id).pipe(
      catchError((error: HttpErrorResponse) => {
        this.router.navigate([`/${namesRoute.TRANSPORTS}`])
        return throwError(error);
      })
    ).subscribe((res) => {
      this.transport = res;
      this.sizeText = `${res.Size.Length ? res.Size.Length : '-'}
      / ${res.Size.Width ? res.Size.Width : '-'}
      / ${res.Size.Height ? res.Size.Height : '-'} м.`;
      this.setTypeImg(this.transport.Type);
    });
  }

  setTypeImg(type: PassengerType) {
    const itemBox = passengerItems.find(item => item.Value === type);
    if (itemBox){
      this.typeImg = this.transportService.getTypeImg(itemBox, TransportType.Passenger);
      this.typeHint = itemBox.Text;
    }
  }

  routeEdit() {
    this.router.navigate([namesRoute.TRANSPORT_EDIT, TransportType.Passenger, this.transport?.Id]);
  }

  get isMyTransport() {
    return this.transport?.UserId === this.identifyService.claims?.Id;
  }
}

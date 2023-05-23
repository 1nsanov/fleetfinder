import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Params, Router} from "@angular/router";
import {CargoTransportApiService} from "../../api/CargoTransport/cargo-transport.api.service";
import {HttpErrorResponse} from "@angular/common/http";
import {catchError, throwError} from "rxjs";
import {CargoTransportGetResponse} from "../../api/CargoTransport/get.models";
import {CargoBodyKindConst,
  CargoTransportationKindConst, ExperienceWorkConst, PaymentMethodConst, PaymentOrderConst, RegionConst} from "../../data/enums.data";
import {TransportType} from "../../models/enums/transport/transport-type.enum";
import {TransportService} from "../../services/transport.service";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";
import {cargoItems} from "../../data/transport/cargo-items";
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {namesRoute} from "../../data/names-route";

@Component({
  selector: 'app-transport-cargo-view-page',
  templateUrl: './transport-cargo-view-page.component.html',
  styleUrls: ['./transport-cargo-view-page.component.scss']
})
export class TransportCargoViewPageComponent implements OnInit{
  RegionConst = RegionConst;
  ExperienceWorkConst = ExperienceWorkConst;
  PaymentMethodConst = PaymentMethodConst;
  PaymentOrderConst = PaymentOrderConst;
  CargoBodyKindConst = CargoBodyKindConst;
  CargoTransportationKindConst = CargoTransportationKindConst;
  TransportType = TransportType;
  typeImg: string | null;
  typeHint: string = "";
  transport: CargoTransportGetResponse | null;
  bodyLengthWidthHeight: string = "";
  constructor(private route: ActivatedRoute,
              private router: Router,
              private cargoTransportService: CargoTransportApiService,
              private identifyService: IdentifyApiService,
              private transportService: TransportService) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) this.getItem(parseInt(id));
    });
  }

  getItem(id: number){
    this.cargoTransportService.get(id).pipe(
      catchError((error: HttpErrorResponse) => {
        this.router.navigate([`/${namesRoute.TRANSPORTS}`])
        return throwError(error);
      })
    ).subscribe((res) => {
      this.transport = res;
      this.bodyLengthWidthHeight = `${res.Body.Length ? res.Body.Length : '-'}
      / ${res.Body.Width ? res.Body.Width : '-'}
      / ${res.Body.Height ? res.Body.Height : '-'} м.`;
      this.setTypeImg(this.transport.Type);
    });
  }

  setTypeImg(type: CargoType) {
    const itemBox = cargoItems.find(item => item.Value === type);
    if (itemBox){
      this.typeImg = this.transportService.getTypeImg(itemBox, TransportType.Cargo);
      this.typeHint = itemBox.Text;
    }
  }

  routeEdit() {
    this.router.navigate([namesRoute.TRANSPORT_EDIT, TransportType.Cargo, this.transport?.Id]);
  }

  get isMyTransport() {
    return this.transport?.UserId === this.identifyService.claims?.Id;
  }
}

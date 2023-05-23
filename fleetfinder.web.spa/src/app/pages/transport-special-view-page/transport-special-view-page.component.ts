import { Component } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {TransportService} from "../../services/transport.service";
import {namesRoute} from "../../data/names-route";
import {ExperienceWorkConst, PaymentOrderConst, PaymentMethodConst, RegionConst } from 'src/app/data/enums.data';
import { TransportType } from 'src/app/models/enums/transport/transport-type.enum';
import {SpecialType} from "../../models/enums/transport/special/special-type.enum";
import {specialItems} from "../../data/transport/special-items";
import {SpecialTransportApiService} from "../../api/SpecialTransport/special-transport.api.service";
import {SpecialTransportGetResponse} from "../../api/SpecialTransport/get.models";
import {catchError, throwError} from "rxjs";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-transport-special-view-page',
  templateUrl: './transport-special-view-page.component.html',
  styleUrls: ['./transport-special-view-page.component.scss']
})
export class TransportSpecialViewPageComponent {
  RegionConst = RegionConst;
  ExperienceWorkConst = ExperienceWorkConst;
  PaymentMethodConst = PaymentMethodConst;
  PaymentOrderConst = PaymentOrderConst;
  TransportType = TransportType;
  typeImg: string | null;
  typeHint: string = "";
  transport: SpecialTransportGetResponse | null;
  constructor(private route: ActivatedRoute,
              private router: Router,
              private specialTransportService: SpecialTransportApiService,
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
    this.specialTransportService.get(id).pipe(
      catchError((error: HttpErrorResponse) => {
        this.router.navigate([`/${namesRoute.TRANSPORTS}`])
        return throwError(error);
      })
    ).subscribe((res) => {
      this.transport = res;
      this.setTypeImg(this.transport.Type);
    });
  }

  setTypeImg(type: SpecialType) {
    const itemBox = specialItems.find(item => item.Value === type);
    if (itemBox){
      this.typeImg = this.transportService.getTypeImg(itemBox, TransportType.Special);
      this.typeHint = itemBox.Text;
    }
  }

  routeEdit() {
    this.router.navigate([namesRoute.TRANSPORT_EDIT, this.transport?.Id]);
  }

  get isMyTransport() {
    return this.transport?.UserId === this.identifyService.claims?.Id;
  }
}

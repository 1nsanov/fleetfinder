import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {CargoTransportApiService} from "../../api/CargoTransport/cargo-transport.api.service";
import {HttpErrorResponse} from "@angular/common/http";
import {catchError, throwError} from "rxjs";
import {CargoTransportGetResponse} from "../../api/CargoTransport/get.models";
import {RegionConst} from "../../data/enums.data";
import {TransportType} from "../../models/enums/transport/transport-type.enum";
import {TransportService} from "../../services/transport.service";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";
import {cargoItems} from "../../data/transport/cargo-items";

@Component({
  selector: 'app-transport-cargo-view-page',
  templateUrl: './transport-cargo-view-page.component.html',
  styleUrls: ['./transport-cargo-view-page.component.scss']
})
export class TransportCargoViewPageComponent implements OnInit{
  RegionConst = RegionConst;
  TransportType = TransportType;
  typeImg: string | null;
  transport: CargoTransportGetResponse | null;
  constructor(private route: ActivatedRoute,
              private cargoTransportService: CargoTransportApiService,
              private transportService: TransportService ) {
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
        return throwError(error);
      })
    ).subscribe((res) => {
      this.transport = res;
      this.setTypeImg(this.transport.Type);
    });
  }

  setTypeImg(type: CargoType){
    const itemBox = cargoItems.find(item => item.Value === type);
    if (itemBox)
      this.typeImg = this.transportService.getTypeImg(itemBox, TransportType.Cargo);
  }
}

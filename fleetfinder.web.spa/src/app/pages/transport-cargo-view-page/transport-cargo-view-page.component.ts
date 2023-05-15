import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {CargoTransportApiService} from "../../api/CargoTransport/cargo-transport.api.service";
import {HttpErrorResponse} from "@angular/common/http";
import {catchError, throwError} from "rxjs";
import {CargoTransportGetResponse} from "../../api/CargoTransport/get.models";

@Component({
  selector: 'app-transport-cargo-view-page',
  templateUrl: './transport-cargo-view-page.component.html',
  styleUrls: ['./transport-cargo-view-page.component.scss']
})
export class TransportCargoViewPageComponent implements OnInit{
  transport: CargoTransportGetResponse | null;
  constructor(private route: ActivatedRoute,
              private cargoTransportService: CargoTransportApiService) {
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
    });
  }
}

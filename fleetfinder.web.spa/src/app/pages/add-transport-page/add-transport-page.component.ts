import { Component } from '@angular/core';
import {
  getCargoBodyKindItems, getCargoTransportationKindItems, getExperienceWorkItems,
  getPaymentMethodItems,
  getPaymentOrderItems,
  getRegionItems, getYearItems
} from "../../data/dropdown-items.data";
import {ModalService} from "../../services/modal.service";
import {TransportType} from "../../models/enums/transport/transport-type.enum";
import {cargoItems} from "../../data/transport/cargo-items";
import {passengerItems} from "../../data/transport/passenger-items";
import {specialItems} from "../../data/transport/special-items";
import {IInfoBoxTransport} from "../../models/interfaces/info-box-transport.interface";
import {CargoType} from "../../models/enums/transport/cargo/cargo-type.enum";
import {CargoTransportPostRequestDto} from "../../api/CargoTransport/post.models";
import {CargoTransportApiService} from "../../api/CargoTransport/cargo-transport.api.service";
import {catchError, throwError} from "rxjs";
import {HttpErrorResponse} from "@angular/common/http";
import {namesRoute} from "../../data/names-route";
import {NotificationService} from "../../services/notification.service";
import {Router} from "@angular/router";
import {Location} from '@angular/common';
import {DropdownItemModel} from "../../models/dropdown-item.model";
import {Region} from "../../models/enums/common/region.enum";
import {CargoTransportationKind} from "../../models/enums/transport/cargo/cargo-transportation-kind";
import {CargoBodyKind} from "../../models/enums/transport/cargo/cargo-body-kind.enum";
import {ExperienceWork} from "../../models/enums/transport/experience-work.enum";
import {PaymentMethod} from "../../models/enums/transport/payment-method.enum";
import {PaymentOrder} from "../../models/enums/transport/payment-order.enum";
import {TransportService} from "../../services/transport.service";

@Component({
  selector: 'app-add-transport-page',
  templateUrl: './add-transport-page.component.html',
  styleUrls: ['./add-transport-page.component.scss']
})
export class AddTransportPageComponent {
  constructor(public modalService: ModalService,
              public cargoTransportApiService: CargoTransportApiService,
              private notification: NotificationService,
              private transportService: TransportService,
              private router: Router,
              private _location: Location) {
  }
  RegionItems = getRegionItems();
  ExperienceWorkItems = getExperienceWorkItems();
  PaymentMethodItems = getPaymentMethodItems();
  PaymentOrderItems = getPaymentOrderItems();
  CargoBodyKindItems = getCargoBodyKindItems();
  CargoTransportationKindItems = getCargoTransportationKindItems();
  YearsItems = getYearItems();
  cargo = cargoItems;
  passenger = passengerItems;
  special = specialItems;
  TransportType = TransportType;
  currentType: TransportType | null = null;
  currentTypeImg : string | null = null;

  cargoTransport : CargoTransportPostRequestDto = new CargoTransportPostRequestDto();
  isLoadPost = false;

  onSelectTransportType(item: IInfoBoxTransport, type: TransportType){
    this.currentType = type;
    this.currentTypeImg = this.transportService.getTypeImg(item, type);
    switch (type){
      case TransportType.Cargo:
        this.cargoTransport.Type = item.Value as CargoType
        break;
      case TransportType.Passenger:
        break;
      case TransportType.Special:
        break;
    }
    this.modalService.close();
  }

  postTransport(){
    this.isLoadPost = true;
    this.cargoTransportApiService.post(this.cargoTransport).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessages = error.error.errors;
        const errorArray = Object.values(errorMessages).flat();
        const errorString = errorArray.join('<br>');
        this.notification.error(errorString);
        this.isLoadPost = false;
        return throwError(error);
      })
    ).subscribe((res) => {
      this.notification.notify('Транспорт успешно добавлен')
      this.router.navigate([namesRoute.transportCargoView, res.Id]);
    });
  }

  onSelectRegion(item: DropdownItemModel<Region>){
    this.cargoTransport.Region = item.Value;
  }
  onSelectYearsItems(item: DropdownItemModel<string>){
    this.cargoTransport.YearIssue = item.Value;
  }
  onSelectTransportationKind(item: DropdownItemModel<CargoTransportationKind>){
    this.cargoTransport.TransportationKind = item.Value;
  }
  onSelectCargoBodyKind(item: DropdownItemModel<CargoBodyKind>){
    this.cargoTransport.Body.Kind = item.Value;
  }
  onSelectExperienceWork(item: DropdownItemModel<ExperienceWork>){
    this.cargoTransport.ExperienceWork = item.Value;
  }
  onSelectPaymentMethod(item: DropdownItemModel<PaymentMethod>){
    this.cargoTransport.PaymentMethod = item.Value;
  }
  onSelectPaymentOrder(item: DropdownItemModel<PaymentOrder>){
    this.cargoTransport.PaymentOrder = item.Value;
  }

  cancel(){
    this._location.back();
  }
}

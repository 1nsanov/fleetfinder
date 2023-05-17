import {Component, OnInit} from '@angular/core';
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
import {CargoTransportForm} from "../../models/interfaces/transport/cargo-transport-form.model";
import {FullName, SignUpModel} from "../../models/interfaces/user/sign-up.model";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {PriceForm} from "../../models/interfaces/transport/price-form.model";
import {BodyForm} from "../../models/interfaces/transport/body-form.model";

@Component({
  selector: 'app-add-transport-page',
  templateUrl: './add-transport-page.component.html',
  styleUrls: ['./add-transport-page.component.scss']
})
export class AddTransportPageComponent implements OnInit{
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
  form: FormGroup<CargoTransportForm>;
  isLoadPost = false;

  constructor(public modalService: ModalService,
              public cargoTransportApiService: CargoTransportApiService,
              private notification: NotificationService,
              private transportService: TransportService,
              private router: Router,
              private _location: Location,
              private formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.initFormBuilder();
  }

  onSelectTransportType(item: IInfoBoxTransport, type: TransportType){
    this.currentType = type;
    this.currentTypeImg = this.transportService.getTypeImg(item, type);
    switch (type){
      case TransportType.Cargo:
        this.form.get('Type')?.setValue(item.Value)
        break;
      case TransportType.Passenger:
        break;
      case TransportType.Special:
        break;
    }
    this.modalService.close();
  }

  postTransport(){
    this.formMarkAsTouched();
    if (this.form.valid){
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
    else
      this.validType();
  }

  onSelectRegion(item: DropdownItemModel<Region>){
    this.form.get('Region')?.setValue(item.Value)
  }
  onSelectYearsItems(item: DropdownItemModel<string>){
    this.form.get('YearIssue')?.setValue(item.Value)
  }
  onSelectTransportationKind(item: DropdownItemModel<CargoTransportationKind>){
    this.form.get('TransportationKind')?.setValue(item.Value)
  }
  onSelectCargoBodyKind(item: DropdownItemModel<CargoBodyKind>){
    this.form.get('Body.Kind')?.setValue(item.Value)
  }
  onSelectExperienceWork(item: DropdownItemModel<ExperienceWork>){
    this.form.get('ExperienceWork')?.setValue(item.Value)
  }
  onSelectPaymentMethod(item: DropdownItemModel<PaymentMethod>){
    this.form.get('PaymentMethod')?.setValue(item.Value)
  }
  onSelectPaymentOrder(item: DropdownItemModel<PaymentOrder>){
    this.form.get('PaymentOrder')?.setValue(item.Value)
  }

  cancel(){
    this._location.back();
  }

  initFormBuilder(){
    this.form = this.formBuilder.group<CargoTransportForm>({
      Title: new FormControl<string | null>('', Validators.required),
      Region: new FormControl<Region | null>(null, Validators.required),
      Type: new FormControl<CargoType | null>(null, Validators.required),
      Brand: new FormControl<string | null>(''),
      YearIssue: new FormControl<string | null>(''),
      ExperienceWork: new FormControl<ExperienceWork | null>(null, Validators.required),
      PaymentMethod: new FormControl<PaymentMethod | null>(null),
      PaymentOrder: new FormControl<PaymentOrder | null>(null),
      Description: new FormControl<string | null>('', Validators.required),
      TransportationKind: new FormControl<CargoTransportationKind | null>(null, Validators.required),
      Price: this.formBuilder.group<PriceForm>({
        PerHour: new FormControl<string | null>(''),
        PerShift: new FormControl<string | null>(''),
        PerKm: new FormControl<string | null>('')
      }),
      Body: this.formBuilder.group<BodyForm>({
        LoadCapacity: new FormControl<number | null>(null),
        Length: new FormControl<number | null>(null),
        Width: new FormControl<number | null>(null),
        Height: new FormControl<number | null>(null),
        Volume: new FormControl<number | null>(null),
        Kind: new FormControl<CargoBodyKind | null>(null),
      }),
      Images: new FormControl<string[] | null>([]),
    });
  }

  formMarkAsTouched(){
    Object.values(this.form.controls).forEach(control => {
      control.markAsTouched();
    });
  }

  validType(){
    if (this.form.get('Type')?.invalid && this.form.get('Type')?.touched)
      this.notification.error('Выберите тип транспорта!')
  }
}

import {Component, OnInit} from '@angular/core';
import {
  getCargoBodyKindItems,
  getCargoTransportationKindItems,
  getExperienceWorkItems,
  getPaymentMethodItems,
  getPaymentOrderItems,
  getRegionItems,
  getYearItems
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
import {ActivatedRoute, Router} from "@angular/router";
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
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {PriceForm} from "../../models/interfaces/transport/price-form.model";
import {BodyForm} from "../../models/interfaces/transport/body-form.model";
import {ModeForm} from "../../models/enums/common/mode-form.enum";
import {CargoTransportGetResponse} from "../../api/CargoTransport/get.models";
import {CargoTransportPutRequestDto} from "../../api/CargoTransport/put.model";

@Component({
  selector: 'app-transport-form-page',
  templateUrl: './transport-form-page.component.html',
  styleUrls: ['./transport-form-page.component.scss']
})
export class TransportFormPageComponent implements OnInit{
  mode: ModeForm = ModeForm.Add;
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

  form: FormGroup<CargoTransportForm>;
  isLoad = false;

  constructor(public modalService: ModalService,
              public cargoTransportApiService: CargoTransportApiService,
              private notification: NotificationService,
              private transportService: TransportService,
              private route: ActivatedRoute,
              private router: Router,
              private _location: Location,
              private formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      this.mode = id ? ModeForm.Edit : ModeForm.Add;
      if (id) {
        this.cargoTransportApiService.get(parseInt(id)).subscribe((res) => {
          this.initFormBuilder(res);
        });
      }
      else
        this.initFormBuilder();
    });
  }

  postTransport(){
    this.formMarkAsTouched();
    if (this.form.valid) {
      if(this.mode === ModeForm.Add)
        this.addTransport();
      else
        this.updateTransport();
    }
    else
      this.validType();
  }

  addTransport(){
    this.isLoad = true;
    const request = this.form.value as CargoTransportPostRequestDto;
    this.cargoTransportApiService.post(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessages = error.error.errors;
        const errorArray = Object.values(errorMessages).flat();
        const errorString = errorArray.join('<br>');
        this.notification.error(errorString);
        this.isLoad = false;
        return throwError(error);
      })
    ).subscribe((res) => {
      this.notification.notify('Транспорт успешно добавлен')
      this.router.navigate([namesRoute.transportCargoView, res.Id]);
    });
  }

  updateTransport(){
    this.isLoad = true;
    const request = this.form.value as CargoTransportPutRequestDto;
    this.cargoTransportApiService.put(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessages = error.error.errors;
        const errorArray = Object.values(errorMessages).flat();
        const errorString = errorArray.join('<br>');
        this.notification.error(errorString);
        this.isLoad = false;
        return throwError(error);
      })
    ).subscribe((res) => {
      this.notification.notify('Транспорт успешно обновлен')
      this.router.navigate([namesRoute.transportCargoView, res.Id]);
    });
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

  initFormBuilder(item: CargoTransportGetResponse | null = null){
    this.form = this.formBuilder.group<CargoTransportForm>({
      Id: new FormControl<number | null>(item?.Id ?? null),
      Title: new FormControl<string | null>(item?.Title ?? null, Validators.required),
      Region: new FormControl<Region | null>(item?.Region ?? null, Validators.required),
      Type: new FormControl<CargoType | null>(item?.Type ?? null, Validators.required),
      Brand: new FormControl<string | null>(item?.Brand ?? null),
      YearIssue: new FormControl<string | null>(item?.YearIssue ?? null),
      ExperienceWork: new FormControl<ExperienceWork | null>(item?.ExperienceWork ?? null),
      PaymentMethod: new FormControl<PaymentMethod | null>(item?.PaymentMethod ?? null),
      PaymentOrder: new FormControl<PaymentOrder | null>(item?.PaymentOrder ?? null),
      Description: new FormControl<string | null>(item?.Description ?? null),
      TransportationKind: new FormControl<CargoTransportationKind | null>(item?.TransportationKind ?? null),
      Price: this.formBuilder.group<PriceForm>({
        PerHour: new FormControl<number | null>(item?.Price.PerHour ?? null),
        PerShift: new FormControl<number | null>(item?.Price.PerShift ?? null),
        PerKm: new FormControl<number | null>(item?.Price.PerKm ?? null)
      }),
      Body: this.formBuilder.group<BodyForm>({
        LoadCapacity: new FormControl<number | null>(item?.Body.LoadCapacity ?? null),
        Length: new FormControl<number | null>(item?.Body.Length ?? null),
        Width: new FormControl<number | null>(item?.Body.Width ?? null),
        Height: new FormControl<number | null>(item?.Body.Height ?? null),
        Volume: new FormControl<number | null>(item?.Body.Volume ?? null),
        Kind: new FormControl<CargoBodyKind | null>(item?.Body.Kind ?? null),
      }),
      Images: new FormControl<string[] | null>(item?.Images ?? []),
    });

    if (item) this.onloadExist(item);
  }

  defaultRegion: DropdownItemModel<Region> | null;
  defaultExperienceWork: DropdownItemModel<ExperienceWork> | null;
  defaultPaymentMethod: DropdownItemModel<PaymentMethod> | null;
  defaultPaymentOrder: DropdownItemModel<PaymentOrder> | null;
  defaultCargoBodyKind: DropdownItemModel<CargoBodyKind> | null;
  defaultCargoTransportationKind: DropdownItemModel<CargoTransportationKind> | null;
  defaultYearIssue: DropdownItemModel<string> | null;
  onloadExist(item: CargoTransportGetResponse){
    const infoBox = this.cargo.find(x => x.Value == item.Type);
    if (infoBox) this.onSelectTransportType(infoBox, TransportType.Cargo)

    this.defaultRegion = this.RegionItems.find(x => x.Value === item.Region) ?? null;
    this.defaultExperienceWork = this.ExperienceWorkItems.find(x => x.Value === item.ExperienceWork) ?? null;
    this.defaultPaymentMethod = this.PaymentMethodItems.find(x => x.Value === item.PaymentMethod) ?? null;
    this.defaultPaymentOrder = this.PaymentOrderItems.find(x => x.Value === item.PaymentOrder) ?? null;
    this.defaultCargoBodyKind = this.CargoBodyKindItems.find(x => x.Value === item.Body.Kind) ?? null;
    this.defaultCargoTransportationKind = this.CargoTransportationKindItems.find(x => x.Value === item.TransportationKind) ?? null;
    this.defaultYearIssue = this.YearsItems.find(x => x.Value === item.YearIssue) ?? null;
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

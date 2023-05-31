import {Component, OnInit} from '@angular/core';
import {
  getCargoBodyKindItems,
  getCargoTransportationKindItems,
  getExperienceWorkItems,
  getPassengerFacilitiesItems,
  getPassengerOptionItems,
  getPassengerRentalDurationItems,
  getPassengerTransportationKindItems,
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
import {TransportForm} from "../../models/interfaces/transport/cargo-transport-form.model";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {PriceForm} from "../../models/interfaces/transport/price-form.model";
import {BodyForm} from "../../models/interfaces/transport/body-form.model";
import {ModeForm} from "../../models/enums/common/mode-form.enum";
import {CargoTransportGetResponse} from "../../api/CargoTransport/get.models";
import {CargoTransportPutRequestDto} from "../../api/CargoTransport/put.model";
import {IdentifyApiService} from "../../api/Identify/identify.api.service";
import {ImagePostRequest} from "../../api/Image/post.models";
import {FirebaseStorageFolder} from "../../models/enums/common/firebase-storage-folder.enum";
import {ImageApiService} from "../../api/Image/image.api.service";
import {ImageDeleteRequest} from "../../api/Image/delete.models";
import {CargoInfoForm} from "../../models/interfaces/transport/cargo-info-form.model";
import {SpecialTransportGetResponse} from "../../api/SpecialTransport/get.models";
import {SpecialInfoForm} from "../../models/interfaces/transport/special-info-form.model";
import {SpecialType} from "../../models/enums/transport/special/special-type.enum";
import {SpecialTransportApiService} from "../../api/SpecialTransport/special-transport.api.service";
import {SpecialTransportPostRequestDto} from "../../api/SpecialTransport/post.models";
import {SpecialTransportPutRequestDto} from "../../api/SpecialTransport/put.model";
import {PassengerTransportPostRequestDto} from "../../api/PassengerTransport/post.models";
import {PassengerType} from "../../models/enums/transport/passenger/passenger-type.enum";
import {PassengerTransportApiService} from "../../api/PassengerTransport/passenger-transport.api.service";
import {PassengerInfoForm} from "../../models/interfaces/transport/passenger-info-form.model";
import {PassengerTransportPutRequestDto} from "../../api/PassengerTransport/put.model";
import {PassengerTransportGetResponse} from "../../api/PassengerTransport/get.models";
import {PassengerRentalDuration} from "../../models/enums/transport/passenger/passenger-rental-duration.enum";
import {PassengerFacilities} from "../../models/enums/transport/passenger/passenger-facilities.enum";
import {PassengerOption} from "../../models/enums/transport/passenger/passenger-option.enum";
import {PassengerTransportationKind} from "../../models/enums/transport/passenger/passenger-transportation-kind.enum";
import {SizeForm} from "../../models/interfaces/transport/size-form.model";
import {PriceModel} from "../../api/Common/Transport/PriceModel";

@Component({
  selector: 'app-transport-form-page',
  templateUrl: './transport-form-page.component.html',
  styleUrls: ['./transport-form-page.component.scss']
})
export class TransportFormPageComponent implements OnInit{
  //#region Consts/Items
  RegionItems = getRegionItems();
  ExperienceWorkItems = getExperienceWorkItems();
  PaymentMethodItems = getPaymentMethodItems();
  PaymentOrderItems = getPaymentOrderItems();
  CargoBodyKindItems = getCargoBodyKindItems();
  CargoTransportationKindItems = getCargoTransportationKindItems();
  PassengerTransportationKindItems = getPassengerTransportationKindItems();
  PassengerRentalDurationItems = getPassengerRentalDurationItems();
  PassengerFacilitiesItems = getPassengerFacilitiesItems();
  PassengerOptionItems = getPassengerOptionItems();
  YearsItems = getYearItems();
  cargo = cargoItems;
  passenger = passengerItems;
  special = specialItems;
  TransportType = TransportType;
  //#endregion

  mode: ModeForm = ModeForm.Add;
  currentType: TransportType | null = null;
  currentTypeImg : string | null = null;
  typeHint: string = "";
  form: FormGroup<TransportForm>;
  cargoInfoForm: FormGroup<CargoInfoForm>;
  passengerInfoForm: FormGroup<PassengerInfoForm>;
  specialInfoForm: FormGroup<SpecialInfoForm>;
  requestImagePost : ImagePostRequest = {
    Folder: FirebaseStorageFolder.CargoTransport,
    Files: []
  }
  requestImageDelete : ImageDeleteRequest = {
    Folder: FirebaseStorageFolder.CargoTransport,
    Urls: []
  }
  isLoad = false;

  constructor(public modalService: ModalService,
              public transportService: TransportService,
              private cargoTransportApiService: CargoTransportApiService,
              private passengerTransportApiService: PassengerTransportApiService,
              private specialTransportApiService: SpecialTransportApiService,
              private identifyService: IdentifyApiService,
              private imageService: ImageApiService,
              private notification: NotificationService,
              private route: ActivatedRoute,
              private router: Router,
              private _location: Location,
              private formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      const type = params.get('type');
      if (type)
        this.currentType = type as TransportType;
      this.mode = id ? ModeForm.Edit : ModeForm.Add;
      if (id) {
        switch (this.currentType){
          case TransportType.Cargo:
            this.cargoTransportApiService.get(parseInt(id)).subscribe((res) => {
              if (res.UserId === this.identifyService.claims?.Id){
                this.initCargoInfoFormBuilder(res);
                this.initFormBuilder(res);
              }
              else
                this.guardRoute();
            });
            break;
          case TransportType.Passenger:
            this.passengerTransportApiService.get(parseInt(id)).subscribe((res) => {
              if (res.UserId === this.identifyService.claims?.Id) {
                this.initPassengerInfoFormBuilder(res);
                this.initFormBuilder(res);
              } else
                this.guardRoute();
            });
            break;
          case TransportType.Special:
            this.specialTransportApiService.get(parseInt(id)).subscribe((res) => {
              if (res.UserId === this.identifyService.claims?.Id) {
                this.initSpecialInfoFormBuilder(res);
                this.initFormBuilder(res);
              }
              else
                this.guardRoute();
            });
            break;
        }
      }
      else{
        this.initFormBuilder();
        this.initCargoInfoFormBuilder();
        this.initPassengerInfoFormBuilder();
        this.initSpecialInfoFormBuilder();
      }
    });
  }

  //#region Api
  async save(){
    this.formMarkAsTouched();
    if (this.form.valid) {
      if(this.mode === ModeForm.Add)
        await this.addTransport();
      else
        await this.updateTransport();
    }
    else
      this.validType();
  }

  async addTransport(){
    this.isLoad = true;

    switch (this.currentType){
      case TransportType.Cargo:
        await this.postCargo();
        break;
      case TransportType.Passenger:
        await this.postPassenger();
        break;
      case TransportType.Special:
        await this.postSpecial();
        break;
    }
  }

  async postCargo(){
    this.requestImagePost.Folder = FirebaseStorageFolder.CargoTransport;
    await this.imageService.upload(this.requestImagePost).then((res) => {
      const request = this.form.value as CargoTransportPostRequestDto;
      request.Type = this.cargoInfoForm.get('Type')?.value as CargoType;
      request.TransportationKind = this.cargoInfoForm.get('TransportationKind')?.value as CargoTransportationKind;
      request.Body = {
        Kind : this.cargoInfoForm.get('Body.Kind')?.value,
        Height : this.cargoInfoForm.get('Body.Height')?.value,
        Volume : this.cargoInfoForm.get('Body.Volume')?.value,
        Length : this.cargoInfoForm.get('Body.Length')?.value,
        Width : this.cargoInfoForm.get('Body.Width')?.value,
        LoadCapacity : this.cargoInfoForm.get('Body.LoadCapacity')?.value,
      };
      request.Images = res;
      this.cargoTransportApiService.post(request).pipe(
        catchError((error: HttpErrorResponse) => {
          this.isLoad = false;
          this.notification.errorFromHttp(error);
          return throwError(error);
        })
      ).subscribe((res) => {
        this.notification.notify('Транспорт успешно добавлен')
        this.router.navigate([namesRoute.TRANSPORT_CARGO_VIEW, res.Id]);
      });
    })
  }

  async postPassenger(){
    this.requestImagePost.Folder = FirebaseStorageFolder.PassengerTransport;
    await this.imageService.upload(this.requestImagePost).then((res) => {
      let request = this.form.value as PassengerTransportPostRequestDto;
      request = this.fillRequestPassenger(request);
      request.Images = res;
      this.passengerTransportApiService.post(request).pipe(
        catchError((error: HttpErrorResponse) => {
          this.isLoad = false;
          this.notification.errorFromHttp(error);
          return throwError(error);
        })
      ).subscribe((res) => {
        this.notification.notify('Транспорт успешно добавлен')
        this.router.navigate([namesRoute.TRANSPORT_PASSENGER_VIEW, res.Id]);
      });
    })
  }

  async postSpecial(){
    this.requestImagePost.Folder = FirebaseStorageFolder.SpecialTransport;
    await this.imageService.upload(this.requestImagePost).then((res) => {
      const request = this.form.value as SpecialTransportPostRequestDto;
      request.Type = this.specialInfoForm.get('Type')?.value as SpecialType;
      request.Images = res;
      this.specialTransportApiService.post(request).pipe(
        catchError((error: HttpErrorResponse) => {
          this.isLoad = false;
          this.notification.errorFromHttp(error);
          return throwError(error);
        })
      ).subscribe((res) => {
        this.notification.notify('Транспорт успешно добавлен')
        this.router.navigate([namesRoute.TRANSPORT_SPECIAL_VIEW, res.Id]);
      });
    })
  }

  async updateTransport(){
    this.isLoad = true;

    switch (this.currentType){
      case TransportType.Cargo:
        await this.putCargo();
        break;
      case TransportType.Passenger:
        await this.putPassenger();
        break;
      case TransportType.Special:
        await this.putSpecial();
        break;
    }
  }

  putCargo(){
    this.requestImagePost.Folder = this.requestImageDelete.Folder = FirebaseStorageFolder.CargoTransport;
    this.imageService.delete(this.requestImageDelete).pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    ).subscribe(async () => {
      await this.imageService.upload(this.requestImagePost).then((res) => {
        const updateImages = this.previewImages.filter(x => x.match("firebase"));
        updateImages.push(...res);
        const request = this.form.value as CargoTransportPutRequestDto;
        request.Type = this.cargoInfoForm.get('Type')?.value as CargoType;
        request.TransportationKind = this.cargoInfoForm.get('TransportationKind')?.value as CargoTransportationKind;
        request.Body = {
          Kind : this.cargoInfoForm.get('Body.Kind')?.value,
          Height : this.cargoInfoForm.get('Body.Height')?.value,
          Volume : this.cargoInfoForm.get('Body.Volume')?.value,
          Length : this.cargoInfoForm.get('Body.Length')?.value,
          Width : this.cargoInfoForm.get('Body.Width')?.value,
          LoadCapacity : this.cargoInfoForm.get('Body.LoadCapacity')?.value,
        };
        request.Images = updateImages;
        this.cargoTransportApiService.put(request).pipe(
          catchError((error: HttpErrorResponse) => {
            this.isLoad = false;
            this.notification.errorFromHttp(error);
            return throwError(error);
          })
        ).subscribe((res) => {
          this.notification.notify('Транспорт успешно обновлен')
          this.router.navigate([namesRoute.TRANSPORT_CARGO_VIEW, res.Id]);
        });
      })
    });
  }

  putPassenger(){
    this.requestImagePost.Folder = this.requestImageDelete.Folder = FirebaseStorageFolder.PassengerTransport;
    this.imageService.delete(this.requestImageDelete).pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    ).subscribe(async () => {
      await this.imageService.upload(this.requestImagePost).then((res) => {
        const updateImages = this.previewImages.filter(x => x.match("firebase"));
        updateImages.push(...res);
        let request = this.form.value as PassengerTransportPutRequestDto;
        request = this.fillRequestPassenger(request) as PassengerTransportPutRequestDto;
        request.Images = updateImages;
        this.passengerTransportApiService.put(request).pipe(
          catchError((error: HttpErrorResponse) => {
            this.isLoad = false;
            this.notification.errorFromHttp(error);
            return throwError(error);
          })
        ).subscribe((res) => {
          this.notification.notify('Транспорт успешно обновлен')
          this.router.navigate([namesRoute.TRANSPORT_PASSENGER_VIEW, res.Id]);
        });
      })
    });
  }

  putSpecial(){
    this.requestImagePost.Folder = this.requestImageDelete.Folder = FirebaseStorageFolder.SpecialTransport;
    this.imageService.delete(this.requestImageDelete).pipe(
      catchError((error: HttpErrorResponse) => {
        return throwError(error);
      })
    ).subscribe(async () => {
      await this.imageService.upload(this.requestImagePost).then((res) => {
        const updateImages = this.previewImages.filter(x => x.match("firebase"));
        updateImages.push(...res);
        const request = this.form.value as SpecialTransportPutRequestDto;
        request.Type = this.specialInfoForm.get('Type')?.value as SpecialType;
        request.Images = updateImages;
        this.specialTransportApiService.put(request).pipe(
          catchError((error: HttpErrorResponse) => {
            this.isLoad = false;
            this.notification.errorFromHttp(error);
            return throwError(error);
          })
        ).subscribe((res) => {
          this.notification.notify('Транспорт успешно обновлен')
          this.router.navigate([namesRoute.TRANSPORT_SPECIAL_VIEW, res.Id]);
        });
      })
    });
  }

  delete(){
    if (this.form.value.Id){
      this.isLoad = true;
      switch (this.currentType){
        case TransportType.Cargo:
          this.cargoTransportApiService.delete(this.form.value.Id).subscribe(() => {
            this.routeDeleteSuccess();
          })
          break;
        case TransportType.Passenger:
          break;
        case TransportType.Special:
          this.specialTransportApiService.delete(this.form.value.Id).subscribe(() => {
            this.routeDeleteSuccess();
          })
          break;
      }
    }
    else
      this.notification.error('Ошибка удаления!')
  }

  routeDeleteSuccess(){
    this.isLoad = false;
    this.router.navigate([`/${namesRoute.TRANSPORTS}`])
      .then(() => {
        this.notification.notify('Транспорт успешно удален')
      })
  }

  fillRequestPassenger(request: PassengerTransportPostRequestDto | PassengerTransportPutRequestDto) {
    request.Type = this.passengerInfoForm.get('Type')?.value as PassengerType;
    request.RentalDuration = this.passengerInfoForm.get('RentalDuration')?.value as PassengerRentalDuration;
    request.Facilities = this.passengerInfoForm.get('Facilities')?.value as PassengerFacilities;
    request.CountSeats = this.passengerInfoForm.get('CountSeats')?.value ?? null;
    request.Option = this.passengerInfoForm.get('Option')?.value as PassengerOption;
    request.TransportationKind = this.passengerInfoForm.get('TransportationKind')?.value as PassengerTransportationKind;
    request.Color = this.passengerInfoForm.get('Color')?.value ?? null;
    request.MinOrderTime = this.passengerInfoForm.get('MinOrderTime')?.value ?? null;
    request.Size = {
      Length: this.passengerInfoForm.get('Length')?.value ?? null,
      Width: this.passengerInfoForm.get('Width')?.value ?? null,
      Height: this.passengerInfoForm.get('Height')?.value ?? null,
    };
    return request;
  }

  convertPriceModel(price: PriceModel) : PriceModel{
    return {
      PerHour: price.PerHour ?? null,
      PerShift: price.PerShift ?? null,
      PerKm: price.PerKm ?? null,
    }
  }

  //#endregion

  //#region Form
  initFormBuilder(item: CargoTransportGetResponse | SpecialTransportGetResponse | PassengerTransportGetResponse | null = null){
    this.form = this.formBuilder.group<TransportForm>({
      Id: new FormControl<number | null>(item?.Id ?? null),
      Title: new FormControl<string | null>(item?.Title ?? null, Validators.required),
      Region: new FormControl<Region | null>(item?.Region ?? null, Validators.required),
      Brand: new FormControl<string | null>(item?.Brand ?? null),
      YearIssue: new FormControl<string | null>(item?.YearIssue ?? null),
      ExperienceWork: new FormControl<ExperienceWork | null>(item?.ExperienceWork ?? null),
      PaymentMethod: new FormControl<PaymentMethod | null>(item?.PaymentMethod ?? null),
      PaymentOrder: new FormControl<PaymentOrder | null>(item?.PaymentOrder ?? null),
      Description: new FormControl<string | null>(item?.Description ?? null),
      Price: this.formBuilder.group<PriceForm>({
        PerHour: new FormControl<number | null>(item?.Price.PerHour ?? null),
        PerShift: new FormControl<number | null>(item?.Price.PerShift ?? null),
        PerKm: new FormControl<number | null>(item?.Price.PerKm ?? null)
      }),
      Images: new FormControl<string[]>(item?.Images ?? []),
    });

    if (item) this.onloadExist(item);
  }

  initCargoInfoFormBuilder(item: CargoTransportGetResponse | null = null) {
    this.cargoInfoForm = this.formBuilder.group<CargoInfoForm>({
      Type: new FormControl<CargoType | null>(item?.Type ?? null, Validators.required),
      TransportationKind: new FormControl<CargoTransportationKind | null>(item?.TransportationKind ?? null),
      Body: this.formBuilder.group<BodyForm>({
        LoadCapacity: new FormControl<number | null>(item?.Body.LoadCapacity ?? null),
        Length: new FormControl<number | null>(item?.Body.Length ?? null),
        Width: new FormControl<number | null>(item?.Body.Width ?? null),
        Height: new FormControl<number | null>(item?.Body.Height ?? null),
        Volume: new FormControl<number | null>(item?.Body.Volume ?? null),
        Kind: new FormControl<CargoBodyKind | null>(item?.Body.Kind ?? null),
      })
    })
  }

  initSpecialInfoFormBuilder(item : SpecialTransportGetResponse | null = null){
    this.specialInfoForm = this.formBuilder.group<SpecialInfoForm>({
      Type: new FormControl<SpecialType | null>(item?.Type ?? null, Validators.required),
    })
  }

  initPassengerInfoFormBuilder(item : PassengerTransportGetResponse | null = null){
    this.passengerInfoForm = this.formBuilder.group<PassengerInfoForm>({
      Type: new FormControl<PassengerType | null>(item?.Type ?? null, Validators.required),
      RentalDuration: new FormControl<PassengerRentalDuration | null>(item?.RentalDuration ?? null),
      Facilities: new FormControl<PassengerFacilities | null>(item?.Facilities ?? null),
      CountSeats: new FormControl<any>(item?.CountSeats ?? null),
      Option: new FormControl<PassengerOption | null>(item?.Option ?? null),
      TransportationKind: new FormControl<PassengerTransportationKind | null>(item?.TransportationKind ?? null),
      Color: new FormControl<string | null>(item?.Color ?? null),
      MinOrderTime: new FormControl<number | null>(item?.MinOrderTime ?? null),
      Size: this.formBuilder.group<SizeForm>({
        Length: new FormControl<number | null>(item?.Size.Length ?? null),
        Width: new FormControl<number | null>(item?.Size.Width ?? null),
        Height: new FormControl<number | null>(item?.Size.Height ?? null),
      })
    })
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
  //#endregion

  //#region valueDropdowns
  onSelectRegion(item: DropdownItemModel<Region>){
    this.form.get('Region')?.setValue(item.Value)
  }
  onSelectYearsItems(item: DropdownItemModel<string>){
    this.form.get('YearIssue')?.setValue(item.Value)
  }
  onSelectTransportationKind(item: DropdownItemModel<CargoTransportationKind>){
    this.cargoInfoForm.get('TransportationKind')?.setValue(item.Value)
  }
  onSelectCargoBodyKind(item: DropdownItemModel<CargoBodyKind>){
    this.cargoInfoForm.get('Body.Kind')?.setValue(item.Value)
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
  onSelectPassengerRentalDuration(item: DropdownItemModel<PassengerRentalDuration | null>){
    this.passengerInfoForm.get('RentalDuration')?.setValue(item.Value)
  }
  onSelectPassengerFacilities(item: DropdownItemModel<PassengerFacilities | null>){
    this.passengerInfoForm.get('Facilities')?.setValue(item.Value)
  }
  onSelectPassengerTransportationKind(item: DropdownItemModel<PassengerTransportationKind | null>){
    this.passengerInfoForm.get('TransportationKind')?.setValue(item.Value)
  }
  onSelectPassengerOption(item: DropdownItemModel<PassengerOption | null>){
    this.passengerInfoForm.get('Option')?.setValue(item.Value)
  }
  valueRegion: DropdownItemModel<Region | null> = this.RegionItems[0];
  valueExperienceWork: DropdownItemModel<ExperienceWork | null> = this.ExperienceWorkItems[0];
  valuePaymentMethod: DropdownItemModel<PaymentMethod | null> = this.PaymentMethodItems[0];
  valuePaymentOrder: DropdownItemModel<PaymentOrder | null> = this.PaymentOrderItems[0];
  valueBodyKind: DropdownItemModel<CargoBodyKind | null> = this.CargoBodyKindItems[0];
  valueTransportationKind: DropdownItemModel<CargoTransportationKind | null> = this.CargoTransportationKindItems[0];
  valuePassengerTransportationKind: DropdownItemModel<PassengerTransportationKind | null> = this.PassengerTransportationKindItems[0];
  valuePassengerRentalDuration: DropdownItemModel<PassengerRentalDuration | null> = this.PassengerRentalDurationItems[0];
  valuePassengerFacilities: DropdownItemModel<PassengerFacilities | null> = this.PassengerFacilitiesItems[0];
  valuePassengerOption: DropdownItemModel<PassengerOption | null> = this.PassengerOptionItems[0];
  valueYearIssue: DropdownItemModel<string> = this.YearsItems[0];

  onloadExist(item: CargoTransportGetResponse | SpecialTransportGetResponse | PassengerTransportGetResponse){
    if (!this.currentType) return;

    if (this.currentType == TransportType.Cargo) {
      const infoBox = this.cargo.find(x => x.Value == item.Type);
      if (infoBox)  this.onSelectTransportType(infoBox, this.currentType)
    }
    else if (this.currentType == TransportType.Passenger) {
      const infoBox = this.passenger.find(x => x.Value == item.Type);
      if (infoBox)  this.onSelectTransportType(infoBox, this.currentType)
    }
    else if (this.currentType == TransportType.Special) {
      const infoBox = this.special.find(x => x.Value == item.Type);
      if (infoBox)  this.onSelectTransportType(infoBox, this.currentType)
    }

    this.valueRegion = this.RegionItems.find(x => x.Value === item.Region) ?? this.RegionItems[0];
    this.valueYearIssue = this.YearsItems.find(x => x.Value === item.YearIssue) ?? this.YearsItems[0];
    this.valueExperienceWork = this.ExperienceWorkItems.find(x => x.Value === item.ExperienceWork) ?? this.ExperienceWorkItems[0];
    this.valuePaymentMethod = this.PaymentMethodItems.find(x => x.Value === item.PaymentMethod) ?? this.PaymentMethodItems[0];
    this.valuePaymentOrder = this.PaymentOrderItems.find(x => x.Value === item.PaymentOrder) ?? this.PaymentOrderItems[0];

    if(item instanceof CargoTransportGetResponse){
      this.valueBodyKind = this.CargoBodyKindItems.find(x => x.Value === item.Body.Kind) ?? this.CargoBodyKindItems[0];
      this.valueTransportationKind = this.CargoTransportationKindItems.find(x => x.Value === item.TransportationKind) ?? this.CargoTransportationKindItems[0];
    }

    this.previewImages = item.Images;
  }

  //#endregion

  //#region Images
  previewFile: File | null = null;
  previewImages: string[] = [];
  onSelectImage(file: File | null) {
    this.previewFile = file;
  }

  onConfirmImage(file: File | null){
    if (!file) return;

    this.requestImagePost.Files.push(file);

    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.previewImages.push(e.target.result);
    };
    reader.readAsDataURL(file);
    this.previewFile = null;
  }

  onRemoveImg(url : string) {
    if (this.mode === ModeForm.Edit)
      this.requestImageDelete.Urls.push(url);
    this.previewImages = this.previewImages.filter(x => x != url);
  }

  onCancelImage(){
    this.previewFile = null;
  }
  //#endregion

  onSelectTransportType(item: IInfoBoxTransport, type: TransportType){
    this.currentType = type;
    this.currentTypeImg = this.transportService.getTypeImg(item, type);
    this.typeHint = item.Text;
    switch (type){
      case TransportType.Cargo:
        this.cargoInfoForm.get('Type')?.setValue(item.Value)
        break;
      case TransportType.Passenger:
        this.initPassengerInfoFormBuilder()
        this.onSelectPassengerRentalDuration(this.PassengerRentalDurationItems[0]);
        this.onSelectPassengerFacilities(this.PassengerFacilitiesItems[0]);
        this.onSelectPassengerTransportationKind(this.PassengerTransportationKindItems[0]);
        this.onSelectPassengerOption(this.PassengerOptionItems[0]);
        this.passengerInfoForm.get('Type')?.setValue(item.Value)
        break;
      case TransportType.Special:
        this.specialInfoForm.get('Type')?.setValue(item.Value)
        break;
    }
    this.modalService.close();
  }

  cancel(){
    this._location.back();
  }

  guardRoute() {
    this.router.navigate([`/${namesRoute.TRANSPORTS}`]).then(() => {
      this.notification.error('Ошибка в доступе. Вы не являетесь создателем данного транспорта')
    });
  }

  get passengerType() : PassengerType {
    return this.passengerInfoForm.get('Type')?.value ?? PassengerType.Taxi;
  }
}

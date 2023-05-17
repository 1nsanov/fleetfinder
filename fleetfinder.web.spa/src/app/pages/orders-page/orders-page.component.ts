import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {CargoTransportItem} from "../../api/Common/Transport/CargoTransportItem";

interface ItemGroup {
  Title: AbstractControl<string | null>
  Brand: AbstractControl<string | null>
}

@Component({
  selector: 'app-orders-page',
  templateUrl: './orders-page.component.html',
  styleUrls: ['./orders-page.component.scss']
})
export class OrdersPageComponent implements OnInit{
  cargoTransportForm: FormGroup<ItemGroup>;
  nf: { [K in keyof any]: string };
  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.cargoTransportForm = this.formBuilder.group<ItemGroup>({
      Title: new FormControl<string| null>('', Validators.required),
      Brand: new FormControl<string| null>('')
    });
    this.nf = this.getPropertyNames(this.cargoTransportForm.value)
  }

  onSubmit(){
    Object.values(this.cargoTransportForm.controls).forEach(control => {
      control.markAsTouched();
    });
    console.log(this.cargoTransportForm.valid)
    if (this.cargoTransportForm.valid){
      const test = this.cargoTransportForm.value as CargoTransportItem;
      console.log(test)
    }
  }

  getPropertyNames<T>(obj: T): { [K in keyof T]: string } {
    const propertyNames: { [K in keyof T]: string } = {} as { [K in keyof T]: string };
    for (const key in obj) {
      propertyNames[key as keyof T] = key;
    }
    return propertyNames;
  }
}

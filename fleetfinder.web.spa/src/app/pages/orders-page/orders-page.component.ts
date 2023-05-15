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
  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.cargoTransportForm = this.formBuilder.group<ItemGroup>({
      Title: new FormControl<string| null>('', Validators.required),
      Brand: new FormControl<string| null>('')
    });
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
}

import {Component, EventEmitter, Input, Output} from '@angular/core';
import { TransportType } from 'src/app/models/enums/transport/transport-type.enum';
import {cargoItems} from "../../../data/transport/cargo-items";
import {passengerItems} from "../../../data/transport/passenger-items";
import {specialItems} from "../../../data/transport/special-items";

@Component({
  selector: 'app-type-transport-sign',
  templateUrl: './type-transport-sign.component.html',
  styleUrls: ['./type-transport-sign.component.scss']
})
export class TypeTransportSignComponent {
  @Input() image : string | null = "../../../assets/icons/icon-square-plus.svg";
  @Input() type: TransportType | null = null;
  @Input() theme: string = "";

  @Output() click = new EventEmitter<void>();
  cargo = cargoItems;
  passenger = passengerItems;
  special = specialItems;
  TransportType = TransportType;

  onClick(){
    this.click.emit();
  }

  get innerImage() : string{
    return this.image ?? "../../../assets/icons/icon-square-plus.svg";
  }
}

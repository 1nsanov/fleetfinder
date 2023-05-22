import {Component, OnInit} from '@angular/core';
import {AbstractControl} from "@angular/forms";

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
  constructor() {}

  ngOnInit() {
  }
}

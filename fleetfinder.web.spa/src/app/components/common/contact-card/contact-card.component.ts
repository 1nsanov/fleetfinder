import {Component, Input} from '@angular/core';
import {Contact} from "../../../api/Common/Contact";

@Component({
  selector: 'app-contact-block',
  templateUrl: './contact-card.component.html',
  styleUrls: ['./contact-card.component.scss']
})
export class ContactCardComponent {
  @Input() contact: Contact;
}

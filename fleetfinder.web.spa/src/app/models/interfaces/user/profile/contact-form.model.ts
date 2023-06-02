import {AbstractControl} from "@angular/forms";

export interface ContactForm {
  PhoneViber : AbstractControl<string | null>;
  PhoneTelegram : AbstractControl<string | null>;
  PhoneWhatsapp : AbstractControl<string | null>;
  WorkingMode : AbstractControl<string | null>;
}

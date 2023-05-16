import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'measurement'
})
export class MeasurementPipe implements PipeTransform {

  transform(value: string|null, code: string): string|null {
    return value ? `${value} ${code}` : null;
  }
}

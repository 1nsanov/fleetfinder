import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'abbreviationName'
})
export class AbbreviationNamePipe implements PipeTransform {
  transform(value: any, ...args: unknown[]): string {
    if (!value) return '';
    const name = value.split(' ');
    let abbr = `${name[0]} ${name[1][0].toUpperCase()}.`;
    if (name[2].length > 0)
        abbr += name[2][0].toUpperCase() + '.';
    return abbr;
  }
}

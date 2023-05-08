export class DropdownItemModel<T> {
  Value: T;
  Preview: string;

  constructor(value : T, preview: string) {
    this.Value = value;
    this.Preview = preview;
  }
}

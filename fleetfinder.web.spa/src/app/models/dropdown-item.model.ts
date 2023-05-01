export class DropdownItemModel<T> {
  Id: number;
  Value: T;
  Preview: string;

  constructor(id: number, value : T, preview: string) {
    this.Id = id;
    this.Value = value;
    this.Preview = preview;
  }
}

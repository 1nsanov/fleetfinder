export class SortModel<TSortParameter>{
  Parameter: TSortParameter;
  ByDesc : boolean;

  constructor(parameter: TSortParameter, byDesc: boolean) {
    this.Parameter = parameter;
    this.ByDesc = byDesc;
  }
}

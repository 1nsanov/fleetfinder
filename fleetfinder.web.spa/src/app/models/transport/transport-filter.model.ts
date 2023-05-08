import {Region} from "../enums/common/region.enum";

export class TransportFilter<TType> {
  UserFilter : number | null = null;
  TitleFilter : string  | null = null;
  RegionFilter : Region | null = null;
  TypeFilter : TType | null = null;
}

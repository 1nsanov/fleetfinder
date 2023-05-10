import {CargoBodyKind} from "../../../models/enums/transport/cargo/cargo-body-kind.enum";

export class BodyModel {
  LoadCapacity: string;
  Length: string;
  Width: string;
  Height: string;
  Volume: string;
  Kind: CargoBodyKind | null = null;
}

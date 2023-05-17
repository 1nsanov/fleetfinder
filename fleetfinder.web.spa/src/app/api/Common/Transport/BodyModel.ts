import {CargoBodyKind} from "../../../models/enums/transport/cargo/cargo-body-kind.enum";

export interface BodyModel {
  LoadCapacity: number | null;
  Length: number | null;
  Width: number | null;
  Height: number | null;
  Volume: number | null;
  Kind: CargoBodyKind | null;
}

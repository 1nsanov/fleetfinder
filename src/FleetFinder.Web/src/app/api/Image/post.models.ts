import {StorageFolder} from "../../models/enums/common/storage-folder.enum";

export interface ImagePostRequest {
  Folder: StorageFolder,
  Files: File[],
}

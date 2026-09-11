import {StorageFolder} from "../../models/enums/common/storage-folder.enum";

export interface ImageDeleteRequest {
  Folder: StorageFolder,
  Urls: string[],
}

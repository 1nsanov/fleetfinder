import {FirebaseStorageFolder} from "../../models/enums/common/firebase-storage-folder.enum";

export interface ImageDeleteRequest {
  Folder: FirebaseStorageFolder,
  Urls: string[],
}

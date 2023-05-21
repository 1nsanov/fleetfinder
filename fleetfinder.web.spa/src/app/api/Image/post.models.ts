import {FirebaseStorageFolder} from "../../models/enums/common/firebase-storage-folder.enum";

export interface ImagePostRequest {
  Folder: FirebaseStorageFolder,
  Files: File[],
}

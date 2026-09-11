import { FullNameForm } from "src/app/models/interfaces/user/sign-up.model";
import {ContactProfile} from "../Common/Contact";

export interface UserProfilePutRequest {
  Email: string;
  Organization: string | null;
  ImageUrl: string | null;
  FullName: {
    First: string;
    Second: string;
    Surname: string | null;
  };
  Contact: ContactProfile;
}

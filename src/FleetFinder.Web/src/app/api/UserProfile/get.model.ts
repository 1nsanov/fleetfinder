import {ContactProfile} from "../Common/Contact";

export interface UserProfileGetResponse {
  Login: string;
  Email: string;
  Organization: string | null;
  ImageUrl: string | null;
  FullName : {
    First: string;
    Second: string;
    Surname: string | null;
  };
  Contact: ContactProfile;
}

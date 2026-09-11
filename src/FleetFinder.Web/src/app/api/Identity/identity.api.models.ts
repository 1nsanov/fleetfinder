import {TokenModel} from "../../models/token.model";

export interface ISignUpRequest{
  Login: string;
  Password: string;
  Email: string;
  Organization: string | null;
  FullName: {
    First: string;
    Second: string;
    Surname: string | null;
  };
}
export interface ITokenResponse{
  Token: TokenModel;
}

export interface ISignInRequest{
  Login: string;
  Password: string;
}

export interface IClaims{
  Id: number | null,
  FullName : string | null
  ImageUrl: string | null;
}

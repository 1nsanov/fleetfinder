import {TokenModel} from "../../models/token.model";

//SignUp
export interface ISignUpRequest{
  Login: string;
  Password: string;
  Email: string;
  Organization: string | null;
  FullName: {
    First: string;
    Second: string;
    Surname: string;
  };
}
export interface ITokenResponse{
  Token: TokenModel;
}

//SignIn
export interface ISignInRequest{
  Login: string;
  Password: string;
}

//Claims
export interface IClaims{
  Id: number | null,
  FullName : string | null
}

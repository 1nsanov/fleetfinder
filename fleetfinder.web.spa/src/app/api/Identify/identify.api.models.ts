//SignUp
import {TokenModel} from "../../models/token.model";

export interface ISignUpRequest{
  Login: string;
  Password: string;
  Email: string;
  Name: {
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

export class SignUpModel {
  Login: string = "";
  Password: string = "";
  RepeatPassword: string = "";
  Email: string = "";
  Name: Name = new Name();
}

class Name{
  First: string = "";
  Second: string = "";
  Surname: string = "";
}

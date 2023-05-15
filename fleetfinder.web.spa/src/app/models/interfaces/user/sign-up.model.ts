export class SignUpModel {
  Login: string = "";
  Password: string = "";
  RepeatPassword: string = "";
  Email: string = "";
  FullName: FullName = new FullName();
  Organization: string | null = null;
}

class FullName{
  First: string = "";
  Second: string = "";
  Surname: string = "";
}


export class Employee {
  public Id: string;
  public FirstName: string;
  public LastName: string;
  public UserName: string;
  public Password: string;
  public Role: string;
}

export class News {
  public Id: string;
  public Content: string;
  public Title: string;
  public PublishDate: Date;
}
export class NewsRequest {
  public Content: string;
  public Title: string;
}

export class NewsResponse {
  public Id: string;
  public Content: string;
  public Title: string;
  public PublishDate: Date;
}

export interface AuthRequest{
  UserName: string;
  Password: string;
}
export interface AuthResponse {
  id: number;
  firstName: string;
  lastName: string;
  userName: string;
  token: string;
}
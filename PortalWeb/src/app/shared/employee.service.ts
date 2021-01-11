import { Injectable } from '@angular/core';
import { Employee } from './message-detail.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  fromDataUser: Employee;
  readonly rootURL = "https://localhost:5001/api";

  employee: Employee[];
  constructor(private http: HttpClient) { }

  postUser(
    fromDataUser: any
  ): Observable<any> {

    var formData: FormData = new FormData();

    formData.append("FirstName", fromDataUser.Firstname);
    formData.append("LastName", fromDataUser.Lastname);

    return this.http.post(this.rootURL + "/Employee/AddEmployee", formData);
  }

  getEmployees<Employee>() {
    return this.http.get(this.rootURL + "/Employee/getEmployees");
  }
}

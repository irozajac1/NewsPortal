import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../shared/employee.service';
import { Employee } from 'src/app/shared/message-detail.model';
import { MatDialog } from "@angular/material";
import {
  faTrash,
  faEdit
} from "node_modules/@fortawesome/free-solid-svg-icons";
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {

  faTrash = faTrash;
  faEdit = faEdit;
  getEmail = localStorage.getItem("upn");
  isAdmin: boolean = false;

  constructor(public EmployeeService: EmployeeService, public dialog: MatDialog, public toastr: ToastrService) {
  }

  Employees: Employee[];

  ngOnInit() {
    this.EmployeeService.getEmployees().subscribe(data => { this.Employees = data as Employee[]; 
      console.log(data)
    });
    this.checkAdmin();
  }

  checkAdmin() {
     {
      this.isAdmin = true;
    }
  }
}


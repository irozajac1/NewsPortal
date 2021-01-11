import { Component, OnInit, Inject, ViewChild } from "@angular/core";
import { MatDialogRef, DateAdapter } from "@angular/material";
import { NgForm, FormGroup, Validators, FormBuilder } from "@angular/forms";
import { NewsService } from "../../shared/news.service";
import { ToastrService } from "ngx-toastr";

import { faWindowClose } from "node_modules/@fortawesome/free-solid-svg-icons";
import { EmployeeService } from '../../shared/employee.service';
import * as jwt_decode from "jwt-decode";

@Component({
  selector: "app-add-message",
  templateUrl: "./add-message.component.html",
  styleUrls: ["./add-message.component.css"],
})
export class AddMessageComponent implements OnInit {

  employeeForm: FormGroup;
  newsForm: FormGroup;
  faWindowClose = faWindowClose;
  getEmail: string;
  isAdmin: boolean = false;

  submitted = false;
  constructor(
    public dialogRef: MatDialogRef<AddMessageComponent>,
    public serviceEmp: EmployeeService,
    public serviceNews: NewsService,
    public toastr: ToastrService,
    private formBuilder: FormBuilder,
    private _adapter: DateAdapter<any>
  ) { }

  ngOnInit() {

    this.getEmail = localStorage.getItem("upn");
    this.resetForm();
    this._adapter.setLocale('hr');
 
    this.checkAdmin();


    // this.employeeForm = this.formBuilder.group({
    //   EmailEmployee: ["", Validators.required],
    //   Firstname: ["", Validators.required],
    //   Lastname: ["", Validators.required],
    //   Telephone: ["", Validators.required],
    //   StartOfWork: ["", Validators.required],
    //   Department: ["", Validators.required],
    //   Position: ["", Validators.required],
    //   EndOfWork: [""],
    //   EmployeePicture: ["", Validators.required],
    // });

    this.newsForm = this.formBuilder.group({
      Title: ["", Validators.required],
      Content: ["", Validators.required],
      PublishDate: ["", Validators.required],
    });

  }
  
  closeClick(): void {
    this.dialogRef.close();
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.resetForm();
    }
  }


  checkAdmin() {
     {
      this.isAdmin = true;
    }
  }

  onSubmitNews() {
    this.serviceNews.postNews(this.newsForm.value).subscribe(res => {
      this.toastr.success("Uspješno");
      this.resetForm();
      this.closeClick();
      location.reload();
    },
      err => {
        this.toastr.error("Pokušajte ponovo", "Došlo je do greške");
      });
  }


}

import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import {
  MatCardModule,
  MatButtonModule,
  MatFormFieldModule,
  MatIconModule,
  MatDialogModule,
  MatToolbarModule,
  MatInputModule,
  MatTooltipModule,
  MatBadgeModule,
  MatSelectModule,
  MatOptionModule,
  MatListModule,
  MatDatepickerModule,
  MatNativeDateModule,
} from "@angular/material";


import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { ToastrModule } from "ngx-toastr";
import { MatExpansionModule } from "@angular/material/expansion";
import {
  MsAdalAngular6Module,
  AuthenticationGuard
} from "microsoft-adal-angular6";
import { ShowMessageComponent } from "./components/show-message/show-message.component";
import { AddMessageComponent } from "./components/add-message/add-message.component";
import { CommonModule } from "@angular/common";
import { NgxSpinnerModule } from "ngx-spinner";
import { Ng2SearchPipeModule } from "ng2-search-filter";
import { MatTabsModule } from "@angular/material/tabs";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { NgxFileDropModule } from "ngx-file-drop";
import { MatChipsModule } from "@angular/material/chips";
import { EmployeesComponent } from './components/employees/employees.component';
import { NewsComponent } from './components/news/news.component';
import { EditNewsComponent } from './components/edit-news/edit-news.component';
import { LoginComponent } from './components/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    ShowMessageComponent,
    AddMessageComponent,
    EmployeesComponent,
    NewsComponent,
    EditNewsComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    NgxSpinnerModule,
    MatButtonModule,
    MatIconModule,
    MatBadgeModule,
    MatDialogModule,
    MatToolbarModule,
    MatTabsModule,
    MatNativeDateModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatListModule,
    MatDatepickerModule,
    FormsModule,
    MatSelectModule,
    MatOptionModule,
    Ng2SearchPipeModule,
    ToastrModule.forRoot(),
    MatExpansionModule,
    MatTooltipModule,
    FontAwesomeModule,
    NgxFileDropModule,
    MatChipsModule
  ],
  entryComponents: [
    AddMessageComponent,
    EditNewsComponent,
  ],
  providers: [
     MatDatepickerModule,
    AuthenticationGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

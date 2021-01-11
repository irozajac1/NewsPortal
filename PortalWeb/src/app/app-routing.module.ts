import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShowMessageComponent } from './components/show-message/show-message.component';
import { EmployeesComponent } from './components/employees/employees.component';
import { NewsComponent } from './components/news/news.component';
import { AuthenticationGuard } from 'microsoft-adal-angular6';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  {
    //
    path: '', component: ShowMessageComponent, 
    children: [
      { path: '', component: NewsComponent },
      { path: 'Employees', component: EmployeesComponent },
      { path: 'Login', component: LoginComponent },
      { path: 'News', component: NewsComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

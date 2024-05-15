import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { FormsModule } from '@angular/forms';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ToastrModule } from 'ngx-toastr';
import { LoginComponent } from './modules/accounts/components/login/login.component';
import { HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { CreateExamComponent } from './modules/exams/components/create-exam/create-exam.component';
import { MyCoursesComponent } from './modules/exams/components/my-courses/my-courses.component';
import { CoursesTableComponent } from './modules/exams/components/courses-table/courses-table.component';
import { ExamsTableComponent } from './modules/exams/components/exams-table/exams-table.component';
import { ExamsComponent } from './modules/exams/components/exams/exams.component';
import { CheckCancellabilityDirective } from './directives/check-cancellability.directive';
import { GetStatusStringDirective } from './directives/get-status-string.directive';
import { DropdownSelectComponent } from './components/dropdown-select/dropdown-select.component';
import { AddAccessTokenInterceptor } from './interceptors/add-access-token.interceptor';
import { LogoutComponent } from './modules/accounts/components/logout/logout.component';
import { StudentExamsComponent } from './modules/exams/components/student-exams/student-exams.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CreateExamComponent,
    MyCoursesComponent,
    CoursesTableComponent,
    ExamsTableComponent,
    ExamsComponent,
    CheckCancellabilityDirective,
    GetStatusStringDirective,
    DropdownSelectComponent,
    LogoutComponent,
    StudentExamsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ToastrModule.forRoot({
      closeButton: true,
    })
    ],
  providers: [
    provideHttpClient(withInterceptors([AddAccessTokenInterceptor]))
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

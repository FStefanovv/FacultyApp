import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import { FormsModule } from '@angular/forms';

import { JwtModule } from "@auth0/angular-jwt";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ToastrModule } from 'ngx-toastr';
import { LoginComponent } from './modules/accounts/components/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { tokenGetter } from './services/auth.service';
import { AuthGuard } from './auth.guard';
import { CreateExamComponent } from './modules/exams/components/create-exam/create-exam.component';
import { MyCoursesComponent } from './modules/exams/components/my-courses/my-courses.component';
import { CoursesTableComponent } from './modules/exams/components/courses-table/courses-table.component';
import { ExamsTableComponent } from './modules/exams/components/exams-table/exams-table.component';
import { ExamsComponent } from './modules/exams/components/exams/exams.component';
import { CheckCancellabilityDirective } from './directives/check-cancellability.directive';
import { GetStatusStringDirective } from './directives/get-status-string.directive';
import { DropdownSelectComponent } from './components/dropdown-select/dropdown-select.component';



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
    DropdownSelectComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001"],
        disallowedRoutes: []
      }
    }),
    BrowserAnimationsModule,
    AppRoutingModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
      closeButton: true,
      toastClass: 'exam-created exam-cancelled' 
    })
    ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }

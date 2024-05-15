import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './modules/accounts/components/login/login.component';
import { CreateExamComponent } from './modules/exams/components/create-exam/create-exam.component';
import { AuthGuard } from './auth.guard';
import { MyCoursesComponent } from './modules/exams/components/my-courses/my-courses.component';
import { ExamsComponent } from './modules/exams/components/exams/exams.component';
import { StudentExamsComponent } from './modules/exams/components/student-exams/student-exams.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent},
  { path: 'create-exam/:courseId', component: CreateExamComponent, canActivate: [AuthGuard], data: { allowedRoles: ['Teacher']} },
  { path: 'create-exam', component: CreateExamComponent, canActivate: [AuthGuard], data: { allowedRoles: ['Teacher']} },
  { path: 'my-courses', component: MyCoursesComponent, canActivate: [AuthGuard], data: { allowedRoles: ['Teacher', 'Student']} },
  { path: 'exams', component: ExamsComponent, canActivate: [AuthGuard], data: { allowedRoles: ['Teacher'] } },
  { path: 'my-exams', component: StudentExamsComponent, canActivate: [AuthGuard], data: { allowedRoles: ['Student'] } }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

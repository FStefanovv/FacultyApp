import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './modules/accounts/components/login/login.component';
import { CreateExamComponent } from './modules/exams/components/create-exam/create-exam.component';
import { authGuard } from './auth.guard';
import { MyCoursesComponent } from './modules/exams/components/my-courses/my-courses.component';
import { TeacherExamsComponent } from './modules/exams/components/teacher-exams/teacher-exams.component';
import { StudentExamsComponent } from './modules/exams/components/student-exams/student-exams.component';
import { ApplyForExamComponent } from './modules/exams/components/apply-for-exam/apply-for-exam.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent},
  { path: 'create-exam/:courseId', component: CreateExamComponent, canActivate: [authGuard], data: { allowedRoles: ['Teacher']} },
  { path: 'create-exam', component: CreateExamComponent, canActivate: [authGuard], data: { allowedRoles: ['Teacher']} },
  { path: 'my-courses', component: MyCoursesComponent, canActivate: [authGuard], data: { allowedRoles: ['Teacher', 'Student']} },
  { path: 'exams', component: TeacherExamsComponent, canActivate: [authGuard], data: { allowedRoles: ['Teacher'] } },
  { path: 'my-exams', component: StudentExamsComponent, canActivate: [authGuard], data: { allowedRoles: ['Student'] } },
  { path: 'apply-exam/:courseId', component: ApplyForExamComponent, canActivate: [authGuard], data: { allowedRoles: ['Student'] } }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

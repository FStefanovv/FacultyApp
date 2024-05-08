import { Component } from '@angular/core';
import { ExamsService } from '../../services/exams.service';
import { Course } from '../../dtos/CourseDto';

@Component({
  selector: 'app-my-courses',
  templateUrl: './my-courses.component.html',
  styleUrl: './my-courses.component.css'
})
export class MyCoursesComponent {

  courses: Course[] = [];

  constructor(private examsService: ExamsService) {}

  ngOnInit(){
    this.fetchCourses();
  }

  fetchCourses() {
    this.examsService.getTeacherCourses().subscribe({
      next: response => {
        this.courses = response;
      },
      error: error => {
        console.log(error.message);
      }
    });
  }
}

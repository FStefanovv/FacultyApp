import { Component, Input, OnInit } from '@angular/core';
import { Course } from '../../dtos/CourseDto';
import { AuthService } from '../../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'courses-table',
  templateUrl: './courses-table.component.html',
  styleUrl: './courses-table.component.css'
})
export class CoursesTableComponent implements OnInit {
 
  @Input()
  courses: Course[] = [];

  role!: string;

  constructor(private authService: AuthService, private router: Router){}

  ngOnInit(): void {
    this.role = this.authService.getRole();
  }

  createExam(courseId: string) {
    this.router.navigate(['/create-exam', courseId]);
  }

}

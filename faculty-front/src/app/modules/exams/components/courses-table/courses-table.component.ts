import { Component, Input, OnInit } from '@angular/core';
import { Course } from '../../dtos/CourseDto';
import { AuthService } from '../../../../services/auth.service';
import { Router } from '@angular/router';
import { SessionService } from '../../../../services/session.service';

@Component({
  selector: 'courses-table',
  templateUrl: './courses-table.component.html',
  styleUrl: './courses-table.component.css'
})
export class CoursesTableComponent implements OnInit {
 
  @Input()
  courses: Course[] = [];

  role!: string;

  constructor(private sessionService: SessionService, private router: Router){}

  ngOnInit(): void {
    const role = this.sessionService.getRole();
    if(role) this.role = role;
  }

  createExam(courseId: string) {
    this.router.navigate(['/create-exam', courseId]);
  }

}

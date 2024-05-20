import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-apply-for-exam',
  templateUrl: './apply-for-exam.component.html',
  styleUrl: './apply-for-exam.component.css'
})
export class ApplyForExamComponent {
  courseId!: string;

  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const routeParamCourseId = this.route.snapshot.paramMap.get('courseId');
    if(routeParamCourseId)
      this.courseId = routeParamCourseId;
    else 
      this.router.navigate(['my-courses']);
  }
}

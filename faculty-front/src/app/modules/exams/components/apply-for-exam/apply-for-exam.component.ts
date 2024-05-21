import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExamsService } from '../../services/exams.service';
import { Examination } from '../../dtos/ExaminationDto';

@Component({
  selector: 'app-apply-for-exam',
  templateUrl: './apply-for-exam.component.html',
  styleUrl: './apply-for-exam.component.css'
})
export class ApplyForExamComponent {
  courseId!: string;
  examinations: Examination[] = [];

  constructor(private examsService: ExamsService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const routeParamCourseId = this.route.snapshot.paramMap.get('courseId');
    if(routeParamCourseId){
      this.courseId = routeParamCourseId;
      this.fetchExams();
    }
    else 
      this.router.navigate(['my-courses']);
  }

  private fetchExams() : void{
    this.examsService.getScheduledExamsForCourse(this.courseId).subscribe({
      next: response => {
        this.examinations = response;
        console.log(this.examinations);
      }, 
      error: error => {
        console.log(error);
      }
    });
  }

}

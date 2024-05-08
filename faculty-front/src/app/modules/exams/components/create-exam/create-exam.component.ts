import { Component, Input, OnInit } from '@angular/core';
import { ExamsService } from '../../services/exams.service';
import { Course } from '../../dtos/CourseDto';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-create-exam',
  templateUrl: './create-exam.component.html',
  styleUrl: './create-exam.component.css'
})

export class CreateExamComponent implements OnInit {
  
  teacherCourses: Course[] = [];

  examDate!: Date;
  courseId!: string;
  numOfPlaces!: number;

  constructor(private examsService: ExamsService, private route: ActivatedRoute){}

  ngOnInit(): void {
    this.examsService.getTeacherCourses().subscribe({
      next: response => {
        this.teacherCourses = response;
      },
      error: error => {
        console.log(error.message);
      }
    });

    this.courseId = this.route.snapshot.params['courseId'];
  } 
  
  createExam() {
    if(!this.courseId || !this.teacherCourses.find(c => c.id === this.courseId)) {
      return;
    }

    if (!this.examDate || this.examDate < new Date()) {
      return;
    }

    if(!this.numOfPlaces || this.numOfPlaces <= 0){
      return;
    }

    this.examsService.create(this.courseId, this.examDate, this.numOfPlaces).subscribe({
      next: (response: any) => {
        console.log('Exam created successfully:', response);
      },
      error : (error: any) => {
        console.error('Error creating exam:', error);
      }
    });

  }
  
  changedCourseSelection(courseId: string){
    this.courseId = courseId;
  }
}

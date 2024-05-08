import { Component, Input, OnInit } from '@angular/core';
import { ExamsService } from '../../services/exams.service';
import { Course } from '../../dtos/CourseDto';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectionItem } from '../../../../models/selectionItem';

@Component({
  selector: 'app-create-exam',
  templateUrl: './create-exam.component.html',
  styleUrl: './create-exam.component.css'
})

export class CreateExamComponent implements OnInit {
  
  teacherCourses: Course[] = [];

  coursesSelectionItems: SelectionItem[] = [];

  examDate!: Date;
  courseId!: string;
  numOfPlaces!: number;

  constructor(private examsService: ExamsService, private route: ActivatedRoute){}

  ngOnInit(): void {
    this.fetchCourses();
  } 

  fetchCourses(){
    this.examsService.getTeacherCourses().subscribe({
      next: response => {
        this.teacherCourses = response;
        this.initializeLocalVars();
      },
      error: error => {
        console.log(error.message);
      }
    });
  }

  initializeLocalVars(){
    this.courseId = this.route.snapshot.params['courseId'];
    this.coursesSelectionItems = this.teacherCourses.map(course => { return new SelectionItem(course.id, course.name)});
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

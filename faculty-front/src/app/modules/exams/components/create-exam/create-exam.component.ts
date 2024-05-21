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
  errorMessage!: string;

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
      this.errorMessage = "Please select a valid course";
      return;
    }

    const examDateTime = new Date(this.examDate);
    const currentDateTime = new Date();

    if (!this.examDate || examDateTime < currentDateTime) {
      console.log('ayy nigga');
      this.errorMessage = "Date must be in the future";
      return;
    }

    if(!this.numOfPlaces || this.numOfPlaces <= 0){
      this.errorMessage = "Entered application slots number must be greater than 0";
      return;
    }

    this.examsService.createExam(this.courseId, this.examDate, this.numOfPlaces).subscribe({
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

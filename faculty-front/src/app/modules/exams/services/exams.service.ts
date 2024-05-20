import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course } from '../dtos/CourseDto';
import { Examination } from '../dtos/ExaminationDto';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {
  
  constructor(private http: HttpClient) {}

  getTeacherCourses() : Observable<Course[]> {
    const teacherCoursesUrl = "https://localhost:5001/my-courses";
   
    return this.http.get<Course[]>(teacherCoursesUrl);
  }

  create(courseId: string, examDate: Date, numOfPlaces: number) : any {
    const createExamUrl = 'https://localhost:5001/exams/' + courseId;
    const newExaminationDto = { scheduledFor: examDate, availablePlaces: numOfPlaces };

    return this.http.post(createExamUrl, newExaminationDto, {  });
  }

  getTeacherExams(filter?: any) : Observable<Examination[]> {
    let getExamsUrl = 'https://localhost:5001/teacher-exams/';
    if(filter !== undefined)
      getExamsUrl +=  filter;
    else {
      getExamsUrl += 'all';
    }

    return this.http.get<Examination[]>(getExamsUrl)
  }

  cancel(id: string) : any {
    const cancelExamUrl = `https://localhost:5001/exams/${id}`;
    return this.http.delete<void>(cancelExamUrl);
  }
}
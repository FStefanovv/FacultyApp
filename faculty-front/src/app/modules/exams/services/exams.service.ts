import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService, tokenGetter } from '../../../services/auth.service';
import { Course } from '../dtos/CourseDto';
import { Examination } from '../dtos/ExaminationDto';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {
  
  constructor(private http: HttpClient) {}

  getBearerTokenHeader() : any {
    const token = tokenGetter();

    let headers = {
      'Authorization': `Bearer ${token}`
    }; 

    return headers;
  }

  getTeacherCourses() : Observable<Course[]> {
    const teacherCoursesUrl = "https://localhost:5001/my-courses";

    let headers = this.getBearerTokenHeader();
   
    return this.http.get<Course[]>(teacherCoursesUrl, { headers } );
  }

  create(courseId: string, examDate: Date, numOfPlaces: number) : any {
    const createExamUrl = 'https://localhost:5001/exams/'+courseId;
    const newExaminationDto = { scheduledFor: examDate, availablePlaces: numOfPlaces };

    let headers = this.getBearerTokenHeader();
    
    return this.http.post(createExamUrl, newExaminationDto, { headers: headers });
  }

  getExams(filter?: any) : Observable<Examination[]> {
    let getExamsUrl = 'https://localhost:5001/exams/filter/';
    if(filter !== undefined)
      getExamsUrl +=  filter;
    else {
      getExamsUrl += 'all';
    }

    let headers = this.getBearerTokenHeader();

    return this.http.get<Examination[]>(getExamsUrl, { headers: headers })
  }

  cancel(id: string) : any{
    const cancelExamUrl = `https://localhost:5001/exams/${id}`;

    let headers = this.getBearerTokenHeader();

    return this.http.delete<void>(cancelExamUrl, { headers });
  }
}
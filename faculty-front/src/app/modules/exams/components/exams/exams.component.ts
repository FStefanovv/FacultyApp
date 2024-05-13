import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { ExamsService } from '../../services/exams.service';
import { Examination } from '../../dtos/ExaminationDto';
import { SessionService } from '../../../../services/session.service';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrl: './exams.component.css'
})
export class ExamsComponent implements OnInit {

  role!: string;
  exams: Examination[] = [];

  constructor(private sessionService: SessionService, private examsService: ExamsService){}

  ngOnInit(): void {
    const role = this.sessionService.getRole();
    if(role)
        this.role = role;
    this.examsService.getExams().subscribe({
      next: response => {
        this.exams = response;
      },
      error: error => {
        console.log(error.message);
      }
    });
  }

  cancel(id: string) {    
    this.examsService.cancel(id).subscribe({
      next: () => {
        let cancelledExam = this.exams.find(exam => exam.id === id);
        if(cancelledExam)
            cancelledExam.status = 1;
      },
      error: () => {
      }
    });
  }
}

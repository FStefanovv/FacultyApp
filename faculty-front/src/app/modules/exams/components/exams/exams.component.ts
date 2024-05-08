import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { ExamsService } from '../../services/exams.service';
import { Examination } from '../../dtos/ExaminationDto';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrl: './exams.component.css'
})
export class ExamsComponent implements OnInit {

  role!: string;
  exams: Examination[] = [];

  constructor(private authService: AuthService, private examsService: ExamsService){}

  ngOnInit(): void {
    this.role = this.authService.getRole();
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

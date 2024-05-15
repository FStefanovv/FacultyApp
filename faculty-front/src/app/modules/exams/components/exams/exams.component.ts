import { Component, OnInit } from '@angular/core';
import { ExamsService } from '../../services/exams.service';
import { Examination } from '../../dtos/ExaminationDto';
import { SessionService } from '../../../../services/session.service';
import { SelectionItem } from '../../../../models/selectionItem';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrl: './exams.component.css'
})
export class ExamsComponent implements OnInit {

  role!: string;
  exams: Examination[] = [];
  selectedFilter: string = "all";
  
  examFilters: SelectionItem[] = [ 
    new SelectionItem('all', 'all'), new SelectionItem('scheduled', 'scheduled') 
  ];

  constructor(private sessionService: SessionService, private examsService: ExamsService){}

  ngOnInit(): void {
    const role = this.sessionService.getRole();
    if(role)
        this.role = role;
    this.examsService.getTeacherExams(this.selectedFilter).subscribe({
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

  filterChanged(filter: string) {
    this.selectedFilter = filter;
    this.examsService.getTeacherExams(this.selectedFilter).subscribe({
      next: response => {
        this.exams = response;
      },
      error: error => {
        console.log(error.message);
      }
    });
  }

}

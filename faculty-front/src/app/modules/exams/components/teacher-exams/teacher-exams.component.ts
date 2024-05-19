import { Component, OnInit } from '@angular/core';
import { ExamsService } from '../../services/exams.service';
import { Examination } from '../../dtos/ExaminationDto';
import { SelectionItem } from '../../../../models/selectionItem';

@Component({
  selector: 'teacher-exams',
  templateUrl: './teacher-exams.component.html',
  styleUrl: './teacher-exams.component.css'
})
export class TeacherExamsComponent implements OnInit {

  exams: Examination[] = [];
  selectedFilter: string = "scheduled";
  
  examFilters: SelectionItem[] = [ 
    new SelectionItem('all', 'all'), new SelectionItem('scheduled', 'scheduled') 
  ];

  constructor(private examsService: ExamsService){}

  ngOnInit(): void {
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
        const examIndex = this.exams.findIndex(e => e.id === id);
        if(examIndex != -1 && this.selectedFilter == 'scheduled'){
          this.exams.splice(examIndex, 1);
        }
        else {
          this.exams[examIndex].status = 1;
          this.sortExams();
        }
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
        if(filter == 'all') this.sortExams();
      },
      error: error => {
        console.log(error.message);
      }
    });
  }

  private sortExams(){
    const futureScheduledExams = this.exams.filter(exam => exam.status === 0 && new Date(exam.scheduledFor) > new Date());
    futureScheduledExams.sort((a, b) => new Date(a.scheduledFor).getTime() - new Date(b.scheduledFor).getTime());

    const otherExams = this.exams.filter(exam => exam.status !== 0 || new Date(exam.scheduledFor) <= new Date());
    otherExams.sort((a, b) => new Date(a.scheduledFor).getTime() - new Date(b.scheduledFor).getTime());

    const sortedExams = futureScheduledExams.concat(otherExams);

    this.exams = sortedExams;
  }

}

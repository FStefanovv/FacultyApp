import { Component } from '@angular/core';
import { ExamsService } from '../../services/exams.service';
import { SelectionItem } from '../../../../models/selectionItem';

@Component({
  selector: 'app-student-exams',
  templateUrl: './student-exams.component.html',
  styleUrl: './student-exams.component.css'
})
export class StudentExamsComponent {

  filter: string = 'scheduled';

  examFilters: SelectionItem[] = [ 
    new SelectionItem('all', 'all'), 
    new SelectionItem('applied', 'applied'),
    new SelectionItem('passed', 'passed'),
    new SelectionItem('failed', 'failed'),
    new SelectionItem('past', 'past')
  ];


  constructor(private examsService: ExamsService){}


}

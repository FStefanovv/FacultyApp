import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { Examination } from '../../dtos/ExaminationDto';
import { AuthService } from '../../../../services/auth.service';
import { CheckCancellabilityDirective } from '../../../../directives/check-cancellability.directive';

@Component({
  selector: 'exams-table',
  templateUrl: './exams-table.component.html',
  styleUrl: './exams-table.component.css'
})
export class ExamsTableComponent implements OnInit {
  @Input()
  examinations: Examination[] = [];

  @Output()
  cancelExamEvent = new EventEmitter<string>();

  role!: string;

  constructor(private authService: AuthService){}
  
  ngOnInit(){
    this.role = this.authService.getRole();
  }

  cancel(id: string) {
    this.cancelExamEvent.emit(id);
  }
}

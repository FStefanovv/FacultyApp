import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Examination } from '../../dtos/ExaminationDto';
import { SessionService } from '../../../../services/session.service';

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

  constructor(private sessionService: SessionService){}
  
  ngOnInit(){
    const role = this.sessionService.getRole();
    if(role) this.role = role;
  }

  cancel(id: string) {
    this.cancelExamEvent.emit(id);
  }
}

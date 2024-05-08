import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[Status]'
})
export class GetStatusStringDirective implements OnInit {

  @Input()
  status!: number;

  @Input()
  examDate!: Date;

  constructor(private el: ElementRef) { }

  ngOnInit(): void {
      this.setStatus()
  }

  ngOnChanges(): void {
    this.setStatus();
  }

  private setStatus() {
    let statusString: string;
    let color: string;

    let twoDaysBeforeExam = new Date(this.examDate);
    twoDaysBeforeExam.setDate(twoDaysBeforeExam.getDate() - 2);

    if(this.status == 1)  {
      statusString = 'cancelled';
      color = 'red';
    }
    else if(this.status == 0 && new Date()<=twoDaysBeforeExam) {
      statusString = 'scheduled';
      color = '#2A525F';
    }
    else if(this.status == 0) {
      statusString = 'past';
      color = '#717171 ';
    }
    else {
      statusString = '';
      color = '';
    }

    this.el.nativeElement.textContent = statusString;
    this.el.nativeElement.style.color = color;
  }
}

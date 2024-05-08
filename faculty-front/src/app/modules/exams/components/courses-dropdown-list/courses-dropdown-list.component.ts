import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Course } from '../../dtos/CourseDto';

@Component({
  selector: 'courses-dropdown-list',
  templateUrl: './courses-dropdown-list.component.html',
  styleUrl: './courses-dropdown-list.component.css'
})
export class CoursesDropdownListComponent implements OnInit {
  @Input()
  courses!: Course[];

  @Input() 
  selectedCourseId: string | undefined;

  @Output()
  selectedCourseEvent = new EventEmitter<string>();

  ngOnInit(): void {
    if(this.selectedCourseId){
      this.selectedCourseEvent.emit(this.selectedCourseId);
    }
  }

  onChange(event: any) {
    const courseId = event.target.value;
    this.selectedCourseEvent.emit(courseId);
  }

}

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoursesDropdownListComponent } from './courses-dropdown-list.component';

describe('CoursesDropdownListComponent', () => {
  let component: CoursesDropdownListComponent;
  let fixture: ComponentFixture<CoursesDropdownListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CoursesDropdownListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CoursesDropdownListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyForExamComponent } from './apply-for-exam.component';

describe('ApplyForExamComponent', () => {
  let component: ApplyForExamComponent;
  let fixture: ComponentFixture<ApplyForExamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ApplyForExamComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApplyForExamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

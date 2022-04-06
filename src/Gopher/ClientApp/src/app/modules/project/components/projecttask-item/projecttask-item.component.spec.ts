import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTaskItemComponent } from './projecttask-item.component';

describe('ProjectTaskItemComponent', () => {
  let component: ProjectTaskItemComponent;
  let fixture: ComponentFixture<ProjectTaskItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectTaskItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectTaskItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

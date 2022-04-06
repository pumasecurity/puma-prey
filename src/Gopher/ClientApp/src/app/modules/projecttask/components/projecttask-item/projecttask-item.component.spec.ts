import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTaskCurrentItemComponent } from './projecttask-item.component';

describe('ProjectTaskItemComponent', () => {
  let component: ProjectTaskCurrentItemComponent;
  let fixture: ComponentFixture<ProjectTaskCurrentItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectTaskCurrentItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectTaskCurrentItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

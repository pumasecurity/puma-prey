import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateProjectTaskComponent } from './create-projecttask.component';

describe('CreateProjectTaskComponent', () => {
  let component: CreateProjectTaskComponent;
  let fixture: ComponentFixture<CreateProjectTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateProjectTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateProjectTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentProjectTaskComponent } from './current-projecttask.component';

describe('CurrentProjectTaskComponent', () => {
  let component: CurrentProjectTaskComponent;
  let fixture: ComponentFixture<CurrentProjectTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CurrentProjectTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CurrentProjectTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

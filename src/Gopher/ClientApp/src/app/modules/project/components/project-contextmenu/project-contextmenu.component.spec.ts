import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectContextmenuComponent } from './project-contextmenu.component';

describe('ProjectContextmenuComponent', () => {
  let component: ProjectContextmenuComponent;
  let fixture: ComponentFixture<ProjectContextmenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProjectContextmenuComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectContextmenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

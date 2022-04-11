import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectUpdateComponent } from './update.component';

describe('ProjectUpdateComponent', () => {
  let component: ProjectUpdateComponent;
  let fixture: ComponentFixture<UpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

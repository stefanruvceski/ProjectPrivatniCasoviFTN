import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ElectrotechnicsComponent } from './electrotechnics.component';

describe('ElectrotechnicsComponent', () => {
  let component: ElectrotechnicsComponent;
  let fixture: ComponentFixture<ElectrotechnicsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ElectrotechnicsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ElectrotechnicsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

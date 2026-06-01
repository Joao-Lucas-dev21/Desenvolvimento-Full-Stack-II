import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Expedicao } from './expedicao';

describe('Expedicao', () => {
  let component: Expedicao;
  let fixture: ComponentFixture<Expedicao>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Expedicao],
    }).compileComponents();

    fixture = TestBed.createComponent(Expedicao);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

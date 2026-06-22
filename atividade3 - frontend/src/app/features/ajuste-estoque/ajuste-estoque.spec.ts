import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { AjusteEstoque } from './ajuste-estoque'; 

describe('AjusteEstoque', () => {
  let component: AjusteEstoque;
  let fixture: ComponentFixture<AjusteEstoque>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({

      imports: [AjusteEstoque, FormsModule],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AjusteEstoque);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
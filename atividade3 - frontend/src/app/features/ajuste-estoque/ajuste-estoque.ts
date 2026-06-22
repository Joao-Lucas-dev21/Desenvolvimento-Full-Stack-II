import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-ajuste-estoque',
  standalone: true,
  imports: [FormsModule], 
  templateUrl: './ajuste-estoque.html', 
  styleUrls: ['./ajuste-estoque.css'] 
})
export class AjusteEstoque {
  produtoId!: number;
  novaQuantidade: number = 0;
  mensagemSucesso: string = '';
  mensagemErro: string = '';

  constructor(private http: HttpClient) {}

  onSubmit() {
    if (this.novaQuantidade < 0) return;
    
    const token = localStorage.getItem('token');
    const headers = {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    };

    this.http.put(`https://localhost:7000/api/produto/${this.produtoId}/estoque`, this.novaQuantidade, { headers })
      .subscribe({
        next: () => {
          this.mensagemSucesso = 'Estoque atualizado com sucesso!';
          this.mensagemErro = '';
        },
        error: (err) => {
          this.mensagemErro = 'Erro ao atualizar estoque.';
          this.mensagemSucesso = '';
        }
      });
  }
}
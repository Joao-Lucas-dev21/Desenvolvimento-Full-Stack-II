import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { ProdutoService } from '../../services/produto-service';
import { Produto } from '../../models/produto';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth-service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-expedicao',
  imports: [AsyncPipe, ReactiveFormsModule],
  templateUrl: './expedicao.html',
  styleUrl: './expedicao.css',
})
export class Expedicao implements OnInit {

  expedicao$!: Observable<Produto[]>;
  errorMSG: string = "Nenhum Produto Encontrado.";
  produtoSelecionado: Produto | null = null;
  itensAdicionados: any[] = [];
  showForm: boolean = false;
  produtoEmEdicao: Produto | null = null; 
  isAdmin: boolean = false;

  form = new FormGroup({
    descricao: new FormControl<string>("", { 
      nonNullable: true,
      validators: [
        Validators.required, 
        Validators.minLength(2),
        Validators.maxLength(50)
      ]
    }),
    Estoque: new FormControl<number>(1, {
      nonNullable: true,
      validators: [
        Validators.required, 
      ]
    }),
  });

  constructor(
    private service: ProdutoService, 
    private authService: AuthService,
    private cdr: ChangeDetectorRef
  ){}
 
  ngOnInit(): void {
    this.load();
  }

  load() {
      if (this.authService.hasRole('Admin')) {
        this.showForm = true;
        this.isAdmin = true; 
      } else {
        this.showForm = false;
        this.isAdmin = false; 
      }

      this.expedicao$ = this.service.getAll().pipe(
        catchError(error => {
          this.errorMSG = "Ocorreu um erro ao carregar os produtos";
          console.log("Erro ao carregar produto: ", error);
          return of([]);
        })
      );
      this.cdr.markForCheck();
    }

  finalizarPedido() {
    const comando = {
      itens: this.itensAdicionados.map(item => ({
        produtoId: item.id,
        quantidade: item.quantidade
      }))
    };


    this.service.finalizarPedido(comando).subscribe({
      next: (res) => {
        alert('Pedido finalizado com sucesso!');
        this.itensAdicionados = []; 
      },
      error: (err) => {
        console.error(err);
        alert('Erro ao finalizar pedido. Verifique o console.');
      }
    });
  }


  adicionarItem(produto: Produto, quantidade: string) {
    const qtd = Number(quantidade);
    this.itensAdicionados.push({
      id: produto.id,
      descricao: produto.descricao,
      quantidade: qtd
    });
  }

  selecionarParaPedido(produto: Produto) {
    this.produtoSelecionado = produto;
    this.showForm = true; 
    
    this.form.patchValue({
      descricao: produto.descricao,
      Estoque: 1 
    });
  }

  editarProduto(produto: Produto) {
    this.produtoEmEdicao = produto;
    this.showForm = true; 

    this.form.patchValue({
      descricao: produto.descricao,
      Estoque: produto.estoque
    });
  }

  cancelarSelecao() {
    this.produtoSelecionado = null;
    this.produtoEmEdicao = null;

    if (!this.authService.hasRole('Admin')) {
      this.showForm = false;
    }

    this.form.reset({
      descricao: '',
      Estoque: 1
    });
  }

  excluirPedido(produto: Produto) {
    const confirmar = confirm(`Deseja realmente excluir ${produto.descricao}?`);
    if (!confirmar) return;

    this.service.delete(produto.id).subscribe({
      next: () => {
        alert('Produto excluído com sucesso!'); 
        this.load(); 
        this.cdr.markForCheck(); 
      },
      error: (err) => {
        console.error(err);
        alert('Erro ao excluir produto');
      }
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    const descricaoValue = this.form.get('descricao')?.value ?? '';
    const estoqueValue = this.form.get('Estoque')?.value ?? 1;

    if (this.produtoEmEdicao) {
      const produtoAtualizado: Produto = {
        id: this.produtoEmEdicao.id,
        descricao: descricaoValue,
        estoque: Number(estoqueValue)
      };

      this.service.update(produtoAtualizado).subscribe({
        next: () => {
          this.load();
          this.cancelarSelecao();
        }
      });
      return;
    }

    if (this.produtoSelecionado) {
      if (Number(estoqueValue) > this.produtoSelecionado.estoque) {
        alert(`Quantidade indisponível! Estoque atual é: ${this.produtoSelecionado.estoque}`);
        return;
      }

      this.itensAdicionados.push({
        id: this.produtoSelecionado.id,
        descricao: this.produtoSelecionado.descricao,
        quantidade: Number(estoqueValue)
      });

      this.cancelarSelecao();
      return;
    }

    this.service.create({
      id: 0,
      descricao: descricaoValue,
      estoque: Number(estoqueValue)
    }).subscribe(() => {
      this.load();
      this.form.reset({
        descricao: '',
        Estoque: 1
      });
    });
  }
}
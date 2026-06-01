import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from '../models/produto';
import { ProdutoCreate } from '../models/produto-create';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ProdutoService {


  private readonly apiUrl = `${environment.apiUrl}/produto`;

  constructor(private http: HttpClient) {}


  private getHeaders() {
    const token = localStorage.getItem('token');
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      })
    };
  }

  getAll() : Observable<Produto[]> {
    return this.http.get<Produto[]>(this.apiUrl, this.getHeaders());
  }

  create(produto: ProdutoCreate) : Observable<Produto> {
    return this.http.post<Produto>(this.apiUrl, produto, this.getHeaders());
  }

  finalizarPedido(comando: any): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/pedido`, comando, this.getHeaders());
  }

  update(produto: Produto) {

    const produtoDTO = {
      descricao: produto.descricao,
      estoque: produto.estoque
    };


    return this.http.put(
      `${this.apiUrl}/${produto.id}`, 
      produtoDTO, 
      this.getHeaders()
    );
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`, this.getHeaders());
  }
}
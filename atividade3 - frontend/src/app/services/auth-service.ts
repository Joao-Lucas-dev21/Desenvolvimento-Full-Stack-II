import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { audit, Observable, take, tap } from 'rxjs';
import { Login } from '../models/login';
import { jwtDecode } from 'jwt-decode';
import { MyTokenPayload } from '../models/my-token-payload';
import { environment } from '../../environments/environment.development';
import { Produto } from '../models/produto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  //private readonly apiUrl = 'https://localhost:7200/api/auth';
  private readonly apiUrl = `${environment.apiUrl}/auth`;

  constructor(private http: HttpClient){}

  login(credentials: Login) : Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login` , credentials)
    .pipe(
      tap(response => {
        if( response.token){
          localStorage.setItem('token', response.token);
          const tokenPayLoad = jwtDecode<MyTokenPayload>(response.token);
          console.log('Role: ' + tokenPayLoad.role);
        }
      })
    )
  }

  logout() {
    localStorage.removeItem('token');
  }

  isAuthenticated() : boolean{
    return !!localStorage.getItem('token');
  }

  hasRole(role: string) : boolean{
    const token = localStorage.getItem('token');
    if (!token)
      return false;
    
    const tokenPayLoad = jwtDecode<MyTokenPayload>(token);
    return tokenPayLoad.role.includes(role);
    
  }

  update(produto: Produto) {
      return this.http.put(
        `${this.apiUrl}/${produto.id}`,
        produto
      );
    }

    delete(id: number) {
      return this.http.delete(
        `${this.apiUrl}/${id}`
      );
    }

}


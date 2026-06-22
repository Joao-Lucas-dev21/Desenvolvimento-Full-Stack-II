import { Routes } from '@angular/router';
import { Expedicao } from './feactures/expedicao/expedicao';
import { Login } from './features/login/login';
import { AjusteEstoque } from './features/ajuste-estoque/ajuste-estoque';

export const routes: Routes = [
    {
        path: '',
        component: Login
    },
    {
        path: 'login',
        component: Login
    },
    {
        path: 'produtos',
        component: Expedicao
    },

    { 
        path: 'ajuste-estoque', 
        component: AjusteEstoque
    }

];

import { Routes } from '@angular/router';
import { Expedicao } from './feactures/expedicao/expedicao';
import { Login } from './features/login/login';

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
    }

];

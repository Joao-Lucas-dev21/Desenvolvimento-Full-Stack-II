import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  form = new FormGroup({
    email : new FormControl<string> ('', {
    nonNullable : true,
    validators: [
      Validators.required,
      Validators.email
    ]
    }),
    password : new FormControl<string> ('', {
      nonNullable: true,
      validators : [
        Validators.required
      ]
    })
  })

  constructor(private service: AuthService, private router: Router){}

    onSubmit() {

      if (this.form.valid){
        this.service.login({email: this.form.value.email!, password: this.form.value.password!})
        .subscribe({
          next: (res) => { 
            console.log("Login efetuado");
            this.router.navigate(['/produtos']);
          },
          error : (err) => alert('Falha na autenticação')
      });
    }
  }
}
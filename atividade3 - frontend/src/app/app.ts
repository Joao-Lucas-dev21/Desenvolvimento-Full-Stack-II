import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Expedicao } from "./feactures/expedicao/expedicao";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Expedicao],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('atividade2');
}

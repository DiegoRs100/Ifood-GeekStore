import { Component, OnInit } from '@angular/core';
import { Produto } from '../models/produto';
import { ProdutoService } from '../services/produto.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-lista',
  templateUrl: './lista.component.html'
})
export class ListaComponent implements OnInit {

  public produtos: Produto[];
  errorMessage: string;

  constructor(private produtoService: ProdutoService) { }

  ngOnInit(): void {
    this.produtoService.obterTodosProdutos()
      .subscribe(
        sucesso => { this.processarSucesso(sucesso) },
        () => this.errorMessage
      );
  }

  processarSucesso(response: any) {

      if (response.success) {
          this.produtos = response.data;
      }
  }
}

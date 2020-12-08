import { Component, OnInit } from '@angular/core';
import { Produto } from '../models/produto';
import { ProdutoService } from '../services/produto.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-lista',
  templateUrl: './lista.component.html'
})
export class ListaComponent implements OnInit {

  public produtos: Produto[];
  errorMessage: string;

  constructor(private produtoService: ProdutoService,
              private spinner: NgxSpinnerService) { }

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
          this.produtos.sort((a, b) => (a.nome < b.nome ? -1 : 1));
      }
  }
}

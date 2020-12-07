import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProdutoService } from '../services/produto.service';

import { ToastrService } from 'ngx-toastr';

import { Produto } from '../models/produto';

@Component({
    selector: 'app-excluir',
    templateUrl: './excluir.component.html'
})
export class ExcluirComponent  {

  public produto: Produto;

  constructor(private produtoService: ProdutoService,
      private route: ActivatedRoute,
      private router: Router,
      private toastr: ToastrService) {

      let response = this.route.snapshot.data['produto'];

      if (response.success && response.data != null) {
          this.produto = this.route.snapshot.data['produto'].data;
      }
  }

  public excluirProduto() {
    this.produtoService.excluirProduto(this.produto.id)
      .subscribe(
          evento => { this.sucessoExclusao(evento) },
          ()     => { this.falha() }
      );
  }

  public sucessoExclusao(evento: any) {

      const toast = this.toastr.success('Produto excluido com Sucesso!', 'Good bye :D');

      if (toast) {
          toast.onHidden.subscribe(() => {
            this.router.navigate(['/produtos']);
          });
      }
  }

  public falha() {
      this.toastr.error('Houve um erro no processamento!', 'Ops! :(');
  }
}

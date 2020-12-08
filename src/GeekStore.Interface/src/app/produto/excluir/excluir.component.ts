import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProdutoService } from '../services/produto.service';

import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

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
      private toastr: ToastrService,
      private spinner: NgxSpinnerService) {

      let response = this.route.snapshot.data['produto'];

      if (response.success && response.data != null) {
          this.produto = this.route.snapshot.data['produto'].data;
      }
  }

  public excluirProduto() {

    this.spinner.show();

    this.produtoService.excluirProduto(this.produto.id)
      .subscribe(
          evento => { this.sucessoExclusao(evento) },
          ()     => { this.falha() }
      );
  }

  public sucessoExclusao(evento: any) {

      setTimeout(() => {

          this.spinner.hide();

          this.router.navigate(['/produtos']);
          this.toastr.success('Produto excluido com Sucesso!', 'Good bye :D');
      }, 2000);
  }

  public falha() {

      setTimeout(() => {
        this.spinner.hide();
        this.toastr.error('Ocorreu um erro!', 'Opa :(');
      }, 2000);
  }
}

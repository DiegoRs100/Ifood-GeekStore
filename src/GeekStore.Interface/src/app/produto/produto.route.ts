import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProdutoAppComponent } from './produto.app.component';
import { ListaComponent } from './lista/lista.component';
import { NovoComponent } from './novo/novo.component';
import { EditarComponent } from './editar/editar.component';
import { ExcluirComponent } from './excluir/excluir.component';
import { ProdutoResolve } from './services/produto.resolve';
import { ProdutoGuard } from './services/produto.guard';

const produtoRouterConfig: Routes = [
    {
        path: '', component: ProdutoAppComponent,
        children: [
            {
                path: '', component: ListaComponent,
                canDeactivate: [ProdutoGuard],
                canActivate: [ProdutoGuard]
            },
            {
                path: 'cadastrar', component: NovoComponent,
                canDeactivate: [ProdutoGuard],
                canActivate: [ProdutoGuard],
            },
            {
                path: 'editar/:id', component: EditarComponent,
                canActivate: [ProdutoGuard],
                resolve: {
                    produto: ProdutoResolve
                }
            },
            {
                path: 'excluir/:id', component: ExcluirComponent,
                canActivate: [ProdutoGuard],
                resolve: {
                    produto: ProdutoResolve
                }
            },
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(produtoRouterConfig)
    ],
    exports: [RouterModule]
})
export class ProdutoRoutingModule { }

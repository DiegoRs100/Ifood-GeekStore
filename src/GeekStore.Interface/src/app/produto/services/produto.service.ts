import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";

import { BaseService } from 'src/app/services/base.service';
import { Produto } from '../models/produto';

@Injectable()
export class ProdutoService extends BaseService {

    constructor(private http: HttpClient) { super() }

    obterTodosProdutos(): Observable<Produto[]> {
        return this.http
            .get<Produto[]>(this.UrlServiceV1 + "produto/obter-todos", super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    obterProdutoPorId(id: string): Observable<Produto> {
        return this.http
            .get<Produto>(this.UrlServiceV1 + "produtos/" + id, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    cadastrarProduto(produto: Produto): Observable<Produto> {
        return this.http
            .post(this.UrlServiceV1 + "produto/cadastrar", produto, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    atualizarProduto(produto: Produto): Observable<Produto> {
        return this.http
            .put(this.UrlServiceV1 + "produto/atualizar" + produto.id, produto, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    excluirProduto(id: string): Observable<Produto> {
        return this.http
            .delete(this.UrlServiceV1 + "produto/excluir" + id, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }
}

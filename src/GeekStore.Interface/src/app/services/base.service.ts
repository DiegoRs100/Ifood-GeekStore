import { HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { throwError } from "rxjs";
import { environment } from 'src/environments/environment';
import { LocalStorageUtils } from '../utils/localstorage';
import { Notificacao } from '../models/notificacao';

export abstract class BaseService {

    protected UrlServiceV1: string = environment.apiUrlv1;
    public LocalStorage = new LocalStorageUtils();

    protected ObterHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }

    protected ObterAuthHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.LocalStorage.obterTokenUsuario()}`
            })
        };
    }

    protected extractData(response: any) {
        return response.data || {};
    }

    protected serviceError(response: Response | any) {
        let customError: Notificacao[] = [];
        let customResponse = { error: { errors: [] }}

        if (response instanceof HttpErrorResponse) {

          if (response.statusText === "Unknown Error") {
                customError.push(new Notificacao("Ocorreu um erro desconhecido", null));
                response.error.errors = customError;
            }
        }
        if (response.status === 500) {
          customError.push(new Notificacao("Ocorreu um erro no processamento, tente novamente mais tarde ou contate o nosso suporte.", null));
            
            // Erros do tipo 500 não possuem uma lista de erros
            // A lista de erros do HttpErrorResponse é readonly                
            customResponse.error.errors = customError;
            return throwError(customResponse);
        }

        console.error(response);
        return throwError(response);
    }
}

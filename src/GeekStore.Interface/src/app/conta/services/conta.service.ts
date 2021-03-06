import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Usuario } from '../models/usuario';

import { Observable } from 'rxjs';
import { catchError, map } from "rxjs/operators";
import { BaseService } from 'src/app/services/base.service';

@Injectable()
export class ContaService extends BaseService {

    constructor(private http: HttpClient) { super(); }

    login(usuario: Usuario): Observable<Usuario> {
        let response = this.http
          .post(this.UrlServiceV1 + 'authentication/login', usuario, this.ObterHeaderJson())
            .pipe(
                map(this.extractData),
                catchError(this.serviceError));

        return response;
    }
}

import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { LocalStorageUtils } from 'src/app/utils/localstorage';


@Injectable()
export class ContaGuard implements CanActivate {
    
    localStorageUtils = new LocalStorageUtils();

    constructor(private router: Router){}
    
    canActivate() {
        if(this.localStorageUtils.obterTokenUsuario()){
            this.router.navigate(['/home']);
        }

        return true;
    }
}

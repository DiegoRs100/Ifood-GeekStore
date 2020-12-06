import { Produto } from './models/produto';
import { FormGroup } from '@angular/forms';
import { ElementRef } from '@angular/core';

import { utilsBr } from 'js-brasil';

import { FormBaseComponent } from '../base-components/form-base.component';

export abstract class ProdutoBaseComponent extends FormBaseComponent {
    
    produto: Produto;
    errors: any[] = [];
    produtoForm: FormGroup;

    MASKS = utilsBr.MASKS;

    constructor() {
        super();

        this.validationMessages = {
            nome: {
                required: 'Informe o Nome',
                minlength: 'Mínimo de 2 caracteres',
                maxlength: 'Máximo de 200 caracteres'
            },
            preco: {
                required: 'Informe o Preço',
            }
        }

        super.configurarMensagensValidacaoBase(this.validationMessages);
    }

    protected configurarValidacaoFormulario(formInputElements: ElementRef[]) {
        super.configurarValidacaoFormularioBase(formInputElements, this.produtoForm);
    }
}

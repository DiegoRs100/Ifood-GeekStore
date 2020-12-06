import { Imagem } from '../../models/imagem';

export interface Produto {

    id: string;

    nome: string;
    preco: number;

    idImagem: string;
    imagem: Imagem;
}

import { Component, OnInit, ViewChildren, ElementRef } from '@angular/core';
import { FormBuilder, Validators, FormControlName } from '@angular/forms';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { ImageCroppedEvent, ImageTransform, Dimensions } from 'ngx-image-cropper';
import { NgxSpinnerService } from 'ngx-spinner';

import { ProdutoService } from '../services/produto.service';
import { CurrencyUtils } from 'src/app/utils/currency-utils';
import { ProdutoBaseComponent } from '../produto-form.base.component';
import { Imagem } from '../../models/imagem';

@Component({
  selector: 'app-novo',
  templateUrl: './novo.component.html'
})
export class NovoComponent extends ProdutoBaseComponent implements OnInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  imageChangedEvent: any = '';
  croppedImage: any = '';
  canvasRotation = 0;
  rotation = 0;
  scale = 1;
  showCropper = false;
  containWithinAspectRatio = false;
  transform: ImageTransform = {};
  imageURL: string;
  imagemNome: string;

  constructor(private fb: FormBuilder,
    private produtoService: ProdutoService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { super(); }

  ngOnInit(): void {

    this.produtoForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      preco: ['', [Validators.required]],
      imagemNome: ['']
    });
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormulario(this.formInputElements);
  }

  cadastrarProduto() {
    if (this.produtoForm.dirty && this.produtoForm.valid) {

      this.spinner.show();

      this.produto = Object.assign({}, this.produto, this.produtoForm.value);

      if (this.imagemNome != null) {

          this.produto.imagem = new Imagem();

          this.produto.imagem.nome = this.imagemNome;
          this.produto.imagem.base64 = this.croppedImage.split(',')[1];
      }

      this.produto.preco = CurrencyUtils.StringParaDecimal(this.produto.preco);

      this.produtoService.cadastrarProduto(this.produto)
        .subscribe(
          sucesso => { this.processarSucesso(sucesso) },
          falha => { this.processarFalha(falha) }
        );

      this.mudancasNaoSalvas = false;
    }
  }

  processarSucesso(response: any) {

    this.produtoForm.reset();
    this.errors = [];

    setTimeout(() => {

      this.spinner.hide();

      this.router.navigate(['/produtos']);
      this.toastr.success('Produto cadastrado com sucesso!', 'Sucesso!');
    }, 2000);
  }

  processarFalha(fail: any) {

    setTimeout(() => {

      this.spinner.hide();

      this.errors = fail.error.errors;
      this.toastr.error('Ocorreu um erro!', 'Opa :(');
    }, 2000);
  }

  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
    this.imagemNome = event.currentTarget.files[0].name;
  }

  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
  }

  imageLoaded() {
    this.showCropper = true;
  }

  cropperReady(sourceImageDimensions: Dimensions) {
    console.log('Cropper ready', sourceImageDimensions);
  }

  loadImageFailed() {
    this.errors.push('O formato do arquivo ' + this.imagemNome + ' não é aceito.');
  }
}

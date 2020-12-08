import { Component, OnInit, ViewChildren, ElementRef } from '@angular/core';
import { FormBuilder, Validators, FormControlName } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { ToastrService } from 'ngx-toastr';

import { ProdutoService } from '../services/produto.service';
import { CurrencyUtils } from 'src/app/utils/currency-utils';
import { ProdutoBaseComponent } from '../produto-form.base.component';
import { ImageTransform, ImageCroppedEvent, Dimensions } from 'ngx-image-cropper';
import { Imagem } from '../../models/imagem';

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html'
})
export class EditarComponent extends ProdutoBaseComponent implements OnInit {

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
  imagePreview: string;
  imagemOriginalSrc: string;

  constructor(private fb: FormBuilder,
    private produtoService: ProdutoService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService) {

    super();

    let response = this.route.snapshot.data['produto'];

    if (response.success && response.data != null) {
      this.produto = this.route.snapshot.data['produto'].data;
    }
  }

  ngOnInit(): void {

    this.produtoForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      preco: ['', [Validators.required]],
      imagemNome: ['']
    });

    this.produtoForm.patchValue({
      id: this.produto.id,
      nome: this.produto.nome,
      preco: CurrencyUtils.DecimalParaString(this.produto.preco)
    });

    if (this.produto.imagem != null) {
      this.imagemOriginalSrc = this.produto.imagem.url;
    }
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormulario(this.formInputElements);
  }

  editarProduto() {

    if (this.produtoForm.dirty && this.produtoForm.valid) {

      this.produto = Object.assign({}, this.produto, this.produtoForm.value);

      if (this.imagemNome != null) {

        this.produto.imagem = new Imagem();

        this.produto.imagem.nome = this.imagemNome;
        this.produto.imagem.base64 = this.croppedImage.split(',')[1];
      }
      else {
        this.produto.imagem = null;
      }


      this.produto.preco = CurrencyUtils.StringParaDecimal(this.produto.preco);

      this.produtoService.atualizarProduto(this.produto)
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

    let toast = this.toastr.success('Produto editado com sucesso!', 'Sucesso!');

    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/produtos']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.errors;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
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

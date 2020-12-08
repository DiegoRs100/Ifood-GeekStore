import { Component, OnInit, ViewChildren, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControlName } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";

import { Usuario } from '../models/usuario';
import { ContaService } from '../services/conta.service';
import { FormBaseComponent } from 'src/app/base-components/form-base.component';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent extends FormBaseComponent implements OnInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  loginForm: FormGroup;
  usuario: Usuario;

  returnUrl: string;

  constructor(private fb: FormBuilder,
    private contaService: ContaService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) {

    super();

    this.validationMessages = {
      login: {
        required: 'Informe o usuário',
      },
      senha: {
        required: 'Informe a senha'
      }
    };

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'];
    super.configurarMensagensValidacaoBase(this.validationMessages);    
  }

  ngOnInit(): void {

    this.loginForm = this.fb.group({
      login: ['', [Validators.required]],
      senha: ['', [Validators.required]]
    });
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.loginForm);
  }

  login() {

    if (this.loginForm.dirty && this.loginForm.valid) {

      this.spinner.show();

      this.usuario = Object.assign({}, this.usuario, this.loginForm.value);
      this.contaService.login(this.usuario)

      .subscribe(
          sucesso => {this.processarSucesso(sucesso)},
          falha => {this.processarFalha(falha)}
      );
    }
  }

  processarSucesso(response: any) {
    this.loginForm.reset();
    this.errors = [];

    this.contaService.LocalStorage.salvarDadosLocaisUsuario(response);

    setTimeout(() => {

      this.spinner.hide();

      this.returnUrl
        ? this.router.navigate([this.returnUrl])
        : this.router.navigate(['/produtos']);

      this.toastr.success('Login realizado com Sucesso!', 'Bem vindo!!!');
    }, 2000);
  }

  processarFalha(fail: any){

    setTimeout(() => {

      this.spinner.hide();

      this.errors = fail.error.errors;
      this.toastr.error('Ocorreu um erro!', 'Opa :(');
    }, 2000);
  }
}

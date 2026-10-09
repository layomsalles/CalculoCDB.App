import { CommonModule } from '@angular/common';
import { Component, inject, signal} from '@angular/core';
import { CdbResponse, Cdb } from '../../Services/cdb';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  imports: [CommonModule,
    FormsModule,
    ReactiveFormsModule],
  selector: 'app-dashboard',
  styleUrl: './dashboard.scss',
  templateUrl: './dashboard.html',
})
export class Dashboard {
  private CdbService = inject(Cdb)

  resultado = signal<CdbResponse | null>(null)

  formDados = new FormGroup({
    valorTotal: new FormControl(0, {
      nonNullable: true,
      validators: [
      Validators.required, Validators.min(1)
    ]
    }),

    mes: new FormControl(6, {
      nonNullable: true,
      validators: [
      Validators.required, Validators.min(1)
    ]
    })
  })

  Calcular(){
    console.log("Dados do formulário:", this.formDados.getRawValue());
    if(this.formDados.invalid){
      this.formDados.markAllAsTouched()
      return
    }

    const {valorTotal, mes} = this.formDados.getRawValue()
    
    const valorInicial = Number(valorTotal);
    const meses = Number(mes);

    if (!Number.isFinite(valorInicial) ||
        !Number.isInteger(meses)) {
      return;
    }

    this.CdbService.Calcular(valorTotal, mes).subscribe({
      next: (response) => {
        this.resultado.set(response)
        console.log(response)

      },
      error: (erro) => {
        console.log(`Erro ao calcular CDB ${erro}`)
      }
    })
  }
}

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../Enviroments/enviroment.development';

export interface CdbResponse{
    valorBruto: number
    valorLiquido: number
}

@Injectable({
    providedIn: 'root'
})

export class Cdb {
    private http = inject(HttpClient)

    private url = `${environment.apiUrl}/api/cdb`

    Calcular(ValorInicial: number, Meses: number){
        return this.http.post<CdbResponse>(
            `${this.url}/calcular`,
            {
                ValorInicial,
                Meses
            }
        )
    }
}

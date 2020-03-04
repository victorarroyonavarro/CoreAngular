import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IPersona } from './persona';

@Injectable()
export class PersonasService {

  private apiUrl = this.baseUrl + "api/personas";
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  listPersonas(): Observable<IPersona[]> {
       return this.http.get<IPersona[]>(this.apiUrl);
  }

  crearPersona(persona: IPersona): Observable<IPersona[]> {
    return this.http.post<IPersona[]>(this.apiUrl + "/crear", persona);
  }

  listPersonabyId(personaId: string): Observable<IPersona> {
    let params=new HttpParams().set('incluirDirecciones',"true")
    return this.http.get<IPersona>(this.apiUrl + '/' + personaId,{params:params});
  }

  ActualizarPersona(persona: IPersona): Observable<IPersona[]> {
    return this.http.put<IPersona[]>(this.apiUrl + "/actualizar", persona);
  }

  deletePersonabyId(personaId: string): Observable<IPersona> {
    return this.http.delete<IPersona>(this.apiUrl + '/' + personaId);
  }

}

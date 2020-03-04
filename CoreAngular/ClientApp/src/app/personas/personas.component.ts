import { Component, OnInit } from '@angular/core';
import { IPersona } from './persona';
import { PersonasService } from './personas.service';

@Component({
  selector: 'app-personas',
  templateUrl: './personas.component.html',
  styleUrls: ['./personas.component.css']
})
export class PersonasComponent implements OnInit {

  personas: IPersona[];

  constructor(private personasService: PersonasService) { }

  ngOnInit() {
    this.cargarData();
  }

  //Api eliminar persona
  delete(persona: IPersona) {
    this.personasService.deletePersonabyId(persona.id.toString())
      .subscribe(persona => this.cargarData()
        , error => console.error(error));

  }

  cargarData() {
    //Lista personas al cargas pagina
    this.personasService.listPersonas()
      .subscribe(personaWs => this.personas = personaWs,
        error => console.error(error));
  }



}
